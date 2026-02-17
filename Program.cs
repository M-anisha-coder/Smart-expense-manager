using Microsoft.Extensions.DependencyInjection;
using SmartExpenseManager.Models;
using SmartExpenseManager.Services;
using Spectre.Console;

// ==============================
// Dependency Injection Setup
// ==============================

var services = new ServiceCollection();

services.AddSingleton<CsvImportService>();
services.AddSingleton<CategorizationService>();
services.AddSingleton<StorageService>();
services.AddSingleton<ReportService>();

var serviceProvider = services.BuildServiceProvider();

var csvService = serviceProvider.GetRequiredService<CsvImportService>();
var categorizationService = serviceProvider.GetRequiredService<CategorizationService>();
var storageService = serviceProvider.GetRequiredService<StorageService>();
var reportService = serviceProvider.GetRequiredService<ReportService>();

// ==============================
// Main Menu Loop
// ==============================

bool exit = false;

while (!exit)
{
    AnsiConsole.Clear();
    AnsiConsole.MarkupLine("[bold cyan]Smart Expense Manager[/]");
    AnsiConsole.WriteLine();

    var choice = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("[green]Select an option[/]")
            .AddChoices(new[]
            {
                "Import & Categorize CSV",
                "Generate Monthly Report",
                "Generate Yearly Summary",
                "Exit"
            }));

    switch (choice)
    {
        case "Import & Categorize CSV":
            ImportAndCategorize(csvService, categorizationService, storageService);
            break;

        case "Generate Monthly Report":
            GenerateMonthlyReport(storageService, reportService);
            break;

        case "Generate Yearly Summary":
            GenerateYearlySummary(storageService, reportService);
            break;

        case "Exit":
            exit = true;
            break;
    }

    if (!exit)
    {
        AnsiConsole.MarkupLine("\n[grey]Press any key to return to menu...[/]");
        Console.ReadKey();
    }
}

AnsiConsole.MarkupLine("[bold green]Goodbye![/]");


// ==============================
// METHODS
// ==============================

static void ImportAndCategorize(
    CsvImportService csvService,
    CategorizationService categorizationService,
    StorageService storageService)
{
    var path = AnsiConsole.Ask<string>("Enter CSV file path:");

    try
    {
        var transactions = csvService.Import(path);

        categorizationService.CategorizeTransactions(transactions);

        storageService.AppendTransactions(transactions);

        AnsiConsole.MarkupLine(
            $"[green]Imported {transactions.Count} transactions successfully.[/]");

        var table = new Table();
        table.AddColumn("Date");
        table.AddColumn("Description");
        table.AddColumn("Amount");
        table.AddColumn("Category");
        table.AddColumn("Business");

        foreach (var t in transactions)
        {
            table.AddRow(
                t.Date.ToShortDateString(),
                t.Description,
                t.Amount.ToString("C"),
                t.Category,
                t.IsBusinessExpense ? "Yes" : "No");
        }

        AnsiConsole.Write(table);
    }
    catch (Exception ex)
    {
        AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
    }
}


static void GenerateMonthlyReport(
    StorageService storageService,
    ReportService reportService)
{
    var transactions = storageService.LoadTransactions();

    if (!transactions.Any())
    {
        AnsiConsole.MarkupLine("[red]No transactions found. Import CSV first.[/]");
        return;
    }

    int year = GetValidYear();
    int month = GetValidMonth();

    var report = reportService.GenerateMonthlyReport(transactions, year, month);

    DisplayReport(report, $"Monthly Report - {month}/{year}");
}


static void GenerateYearlySummary(
    StorageService storageService,
    ReportService reportService)
{
    var transactions = storageService.LoadTransactions();

    if (!transactions.Any())
    {
        AnsiConsole.MarkupLine("[red]No transactions found. Import CSV first.[/]");
        return;
    }

    int year = GetValidYear();

    var report = reportService.GenerateYearlyReport(transactions, year);

    DisplayReport(report, $"Yearly Report - {year}");
}


// ==============================
// Helper Methods
// ==============================

static int GetValidYear()
{
    while (true)
    {
        int year = AnsiConsole.Ask<int>("Enter Year (e.g., 2026):");

        if (year >= 2000 && year <= DateTime.Now.Year)
            return year;

        AnsiConsole.MarkupLine("[red]Invalid year. Try again.[/]");
    }
}

static int GetValidMonth()
{
    while (true)
    {
        int month = AnsiConsole.Ask<int>("Enter Month (1-12):");

        if (month >= 1 && month <= 12)
            return month;

        AnsiConsole.MarkupLine("[red]Invalid month. Try again.[/]");
    }
}

static void DisplayReport(MonthlyReport report, string title)
{
    AnsiConsole.MarkupLine($"\n[bold yellow]{title}[/]\n");

    var summaryTable = new Table();
    summaryTable.AddColumn("Metric");
    summaryTable.AddColumn("Value");

    summaryTable.AddRow("Total Income", report.TotalIncome.ToString("C"));
    summaryTable.AddRow("Total Expenses", report.TotalExpenses.ToString("C"));
    summaryTable.AddRow("Net Profit", report.NetProfit.ToString("C"));
    summaryTable.AddRow("Business Expenses", report.BusinessExpenses.ToString("C"));

    AnsiConsole.Write(summaryTable);

    AnsiConsole.MarkupLine("\n[bold]Category Breakdown[/]");

    var categoryTable = new Table();
    categoryTable.AddColumn("Category");
    categoryTable.AddColumn("Amount");

    foreach (var category in report.CategorySummaries)
    {
        categoryTable.AddRow(
            category.Category,
            category.TotalAmount.ToString("C"));
    }

    AnsiConsole.Write(categoryTable);
}
