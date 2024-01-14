using FluentAssertions;
using InvoiceApp.WebApi;
using InvoiceApp.WebApi.Models;
using InvoiceApp.WebApi.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net.Mail;

namespace InvoiceApp.UnitTests;

public class EmailServiceTests
{
    private readonly Mock<ILogger<IEmailService>> _loggerMock = new();
    private readonly Mock<IEmailSender> _emailSenderMock = new();

    [Fact]
    public void GenerateInvoiceEmail_Should_Return_Email()
    {
        var invoiceDate = DateTimeOffset.Now;
        var dueDate = invoiceDate.AddDays(30);
        // Arrange
        var invoice = new Invoice
        {
            Id = Guid.NewGuid(),
            InvoiceNumber = "INV-001",
            Amount = 500,
            DueDate = dueDate,
            Contact = new Contact
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            },
            Status = InvoiceStatus.Draft,
            InvoiceDate = invoiceDate,
            InvoiceItems = new List<InvoiceItem>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Description = "Item 1",
                    Quantity = 1,
                    UnitPrice = 100,
                    Amount = 100
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Description = "Item 2",
                    Quantity = 2,
                    UnitPrice = 200,
                    Amount = 400
                }
            }
        };

        // Act
        var (to, subject, body) = new EmailService(_loggerMock.Object, _emailSenderMock.Object).GenerateInvoiceEmail(invoice);

        // Assert
        Assert.Equal(invoice.Contact.Email, to);
        Assert.Equal($"Invoice INV-001 for John Doe", subject);
        Assert.Equal($"""
            Dear John Doe,

            Thank you for your business. Here are your invoice details:
            Invoice Number: INV-001
            Invoice Date: {invoiceDate.LocalDateTime.ToShortDateString()}
            Invoice Amount: {invoice.Amount.ToString("C")}
            Invoice Items:
            Item 1 - 1 x $100.00
            Item 2 - 2 x $200.00
            
            Please pay by {invoice.DueDate.LocalDateTime.ToShortDateString()}. Thank you!

            Regards,
            InvoiceApp
            """, body);
    }

    [Fact]
    public async Task SendEmailAsync_Should_Send_Email()
    {
        // Arrange
        var to = "user@example.com";
        var subject = "Test Email";
        var body = "Hello, this is a test email";

        _emailSenderMock.Setup(m => m.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Task.CompletedTask);
        _loggerMock.Setup(l => l.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(),
            It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable();
        var emailService = new EmailService(_loggerMock.Object, _emailSenderMock.Object);

        // Act
        await emailService.SendEmailAsync(to, subject, body);

        // Assert
        _emailSenderMock.Verify(m => m.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        _loggerMock.Verify(
            l => l.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"Sending email to {to} with subject {subject} and body {body}")),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()
            ),
            Times.Once
        );
        _loggerMock.Verify(
            l => l.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"Email sent to {to} with subject {subject}")),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()
            ),
            Times.Once
        );
    }

    [Fact]
    public async Task SendEmailAsync_Should_Log_SmtpException()
    {
        // Arrange
        var to = "user@example.com";
        var subject = "Test Email";
        var body = "Hello, this is a test email";

        _emailSenderMock.Setup(m => m.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ThrowsAsync(new SmtpException("Test SmtpException"));
        _loggerMock.Setup(l => l.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(),
                       It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable();
        var emailService = new EmailService(_loggerMock.Object, _emailSenderMock.Object);

        // Act + Assert
        //await Assert.ThrowsAsync<SmtpException>(() => emailService.SendEmailAsync(to, subject, body));
        var act = () => emailService.SendEmailAsync(to, subject, body);
        await act.Should().ThrowAsync<SmtpException>().WithMessage("Test SmtpException");

        _loggerMock.Verify(
            l => l.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) =>
                    v.ToString().Contains($"Sending email to {to} with subject {subject} and body {body}")),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()
            ),
            Times.Once
        );
        _loggerMock.Verify(
            l => l.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) =>
                    v.ToString().Contains($"Failed to send email to {to} with subject {subject}")),
                It.IsAny<SmtpException>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()
            ),
            Times.Once
        );
    }
}