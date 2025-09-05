using System.Globalization;
using System.Text;

static void GenerateSalesSummary(string salesDataDir, string reportPath)
{
    if (!Directory.Exists(salesDataDir))
    {
        Console.WriteLine($"Sales directory not found: {salesDataDir}");
        return;
    }

    string[] files = Directory.GetFiles(salesDataDir, "*.*", SearchOption.AllDirectories);

    var sb = new StringBuilder();
    decimal grandTotal = 0m;
    var details = new List<(string FileName, decimal Total)>();

    foreach (var file in files)
    {
        var attr = File.GetAttributes(file);
        if ((attr & FileAttributes.Directory) == FileAttributes.Directory) continue;

        decimal sum = 0m;

        foreach (var raw in File.ReadLines(file))
        {
            var line = raw?.Trim();
            if (string.IsNullOrWhiteSpace(line)) continue;

            if (decimal.TryParse(line, NumberStyles.Currency | NumberStyles.Number, CultureInfo.CurrentCulture, out var value) ||
                decimal.TryParse(line, NumberStyles.Currency | NumberStyles.Number, CultureInfo.InvariantCulture, out value))
            {
                sum += value;
            }
        }

        details.Add((Path.GetFileName(file), sum));
        grandTotal += sum;
    }

    sb.AppendLine("Sales Summary");
    sb.AppendLine("----------------------------");
    sb.AppendLine($" Total Sales: {grandTotal:C}");
    sb.AppendLine();
    sb.AppendLine(" Details:");

    foreach (var (FileName, Total) in details.OrderByDescending(d => d.Total))
        sb.AppendLine($"  {FileName}: {Total:C}");

    Directory.CreateDirectory(Path.GetDirectoryName(reportPath) ?? ".");
    File.WriteAllText(reportPath, sb.ToString());

    Console.WriteLine($"Sales summary written to: {reportPath}");
}

// ---- Call it (keep this at the bottom of Program.cs) ----
string salesDir   = Path.Combine(Environment.CurrentDirectory, "stores");
string reportPath = Path.Combine(Environment.CurrentDirectory, "sales-summary.txt");

GenerateSalesSummary(salesDir, reportPath);

