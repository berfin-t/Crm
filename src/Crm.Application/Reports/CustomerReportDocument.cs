using Crm.Activities;
using Crm.CustomerNotes;
using Crm.Customers;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;

public class CustomerReportDocument : IDocument
{
    private readonly CustomerDto _customer;
    private readonly List<CustomerNoteDto> _notes;
    private readonly List<ActivityDto> _activities;

    public CustomerReportDocument(CustomerDto customer, List<CustomerNoteDto> notes, List<ActivityDto> activities)
    {
        _customer = customer;
        _notes = notes ?? new List<CustomerNoteDto>();
        _activities = activities ?? new List<ActivityDto>();
    }

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(30);
            page.DefaultTextStyle(x => x.FontSize(11).FontColor("#333333"));

            page.Header().Element(ComposeHeader);
            page.Content().Element(ComposeContent);
            page.Footer().AlignCenter().Text(x =>
            {
                x.Span("CRM System • ").SemiBold();
                x.Span($"{DateTime.Now:dd.MM.yyyy}");
            });
        });
    }

    private void ComposeHeader(IContainer container)
    {
        container
            .Background("#1E3A8A")
            .Padding(15)
            .Row(row =>
            {
                row.RelativeItem().Column(col =>
                {
                    col.Item().Text("CUSTOMER REPORT")
                        .FontSize(20)
                        .Bold()
                        .FontColor("#FFFFFF");

                    col.Item().Text($"{_customer?.Name} {_customer?.Surname}")
                        .FontSize(14)
                        .FontColor("#E0E7FF");

                    col.Item().Text($"Created: {DateTime.Now:dd.MM.yyyy}")
                        .FontSize(10)
                        .FontColor("#CBD5E1");
                });

                row.ConstantItem(180).AlignRight().Column(right =>
                {
                    right.Item().Text($"Company: {_customer?.CompanyName ?? "-"}")
                        .FontSize(10).FontColor("#E0E7FF");
                    right.Item().Text($"Email: {_customer?.Email ?? "-"}")
                        .FontSize(10).FontColor("#E0E7FF");
                });
            });
    }

    private void ComposeContent(IContainer container)
    {     
        container.PaddingVertical(10).Column(col =>
        {
            col.Item().Element(ComposeCustomerInfo);
            col.Item().PaddingTop(12).Element(ComposeNotes);
            col.Item().PaddingTop(12).Element(ComposeActivities);
        });
    }

    private void ComposeCustomerInfo(IContainer container)
    {
        container.Padding(15)
                 .Background("#F1F5F9")
                 .Border(1)
                 .BorderColor("#CBD5E1")
                 .Column(col =>
                 {
                     col.Item().Text("Customer Information")
                         .FontSize(16)
                         .Bold()
                         .FontColor("#1E3A8A")
                         .Underline();

                     col.Item().PaddingTop(10).Row(row =>
                     {
                         row.RelativeItem().Column(left =>
                         {
                             left.Item().Text($"Full Name: {_customer?.FullName ?? "-"}");
                             left.Item().Text($"Email: {_customer?.Email ?? "-"}");
                             left.Item().Text($"Phone: {_customer?.Phone ?? "-"}");
                         });

                         row.RelativeItem().Column(right =>
                         {
                             right.Item().Text($"Created On: {_customer?.CreationTime:dd.MM.yyyy}");
                             right.Item().Text($"Company: {_customer?.CompanyName ?? "-"}");
                             right.Item().Text($"Address: {_customer?.Address ?? "-"}");
                         });
                     });
                 });
    }

    private void ComposeNotes(IContainer container)
    {
        container.Column(col =>
        {
            col.Item().Text("Notes")
                .FontSize(16)
                .Bold()
                .FontColor("#1E40AF");

            if (_notes.Count == 0)
            {
                col.Item().Text("No notes available.").Italic();
                return;
            }

            foreach (var note in _notes)
            {
                col.Item().Padding(6)
                          .Background("#FFFFFF")
                          .Border(1)
                          .BorderColor("#E2E8F0")
                          .Column(c =>
                          {
                              c.Item().Row(r =>
                              {
                                  r.ConstantItem(28).AlignMiddle().Text("📝").FontSize(12);

                                  r.RelativeItem().Column(nc =>
                                  {
                                      nc.Item().Text(note.Note ?? string.Empty).FontSize(12);
                                      nc.Item().AlignRight().Text(note.NoteDate.ToString("dd.MM.yyyy HH:mm"))
                                          .FontSize(10).FontColor("#64748B");
                                  });
                              });
                          });

                col.Item().PaddingBottom(6);
            }
        });
    }

    private void ComposeActivities(IContainer container)
    {
        container.Column(col =>
        {
            col.Item().Text("Activities")
                .FontSize(16)
                .Bold()
                .FontColor("#1E40AF");

            if (_activities.Count == 0)
            {
                col.Item().Text("No activities available.").Italic();
                return;
            }

            col.Item().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(28);   // ikon
                    columns.ConstantColumn(36);   // sıra no
                    columns.RelativeColumn(3);    // açıklama
                    columns.RelativeColumn(1);    // tip
                    columns.RelativeColumn(1);    // tarih
                });

                table.Header(header =>
                {
                    header.Cell().Element(HeaderCell).Text("");
                    header.Cell().Element(HeaderCell).Text("#");
                    header.Cell().Element(HeaderCell).Text("Description");
                    header.Cell().Element(HeaderCell).Text("Type");
                    header.Cell().Element(HeaderCell).Text("Date");
                });

                var index = 1;
                foreach (var a in _activities)
                {
                    var icon = GetActivityIcon(a.Type.ToString());

                    table.Cell().Element(Cell).Text(icon);
                    table.Cell().Element(Cell).Text(index++.ToString());
                    table.Cell().Element(Cell).Text((a.Description ?? "-").Trim());
                    table.Cell().Element(Cell).Text(a.Type.ToString() ?? "-");
                    table.Cell().Element(Cell).Text(a.Date.ToString("dd.MM.yyyy"));
                }
            });
        });
    }

    IContainer HeaderCell(IContainer container)
    {
        return container.Padding(6)
                        .Background("#E0E7FF")
                        .BorderBottom(1)
                        .BorderColor("#1E3A8A");
    }

    IContainer Cell(IContainer container)
    {
        return container.Padding(6)
                        .BorderBottom(1)
                        .BorderColor("#E2E8F0");
    }

    private string GetActivityIcon(string type)
    {
        if (string.IsNullOrEmpty(type)) return "🔔";

        type = type.ToLowerInvariant();
        if (type.Contains("call") || type.Contains("arama")) return "📞";
        if (type.Contains("email")) return "✉️";
        if (type.Contains("meeting") || type.Contains("toplant")) return "🤝";
        if (type.Contains("task")) return "✅";
        if (type.Contains("note")) return "📝";
        return "🔔";
    }
}
