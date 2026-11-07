using Microsoft.Extensions.Logging;
using Src.Domain.NewsFeed;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Src.Application.Features.Newsfeed.NewFolder;

public class SikhnetNewsFeedHelper
{
    private readonly ILogger<SikhnetNewsFeedHelper> _logger;
    private readonly HttpClient _httpClient;

    public SikhnetNewsFeedHelper(ILogger<SikhnetNewsFeedHelper> logger, HttpClient httpClient)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<SikhnetNewsRecord?> getNewestArticle(CancellationToken cancellationToken)
    {
        string xml = await _httpClient.GetStringAsync("https://sikhnet.com/feed/news", cancellationToken);
        var sikhRecords = await xmlToObject(xml, cancellationToken);

        if (!sikhRecords.Any())
        {
            return null;
        }

        var newestRecord = sikhRecords.OrderByDescending(sr => sr.PublicationDate).First();
        return newestRecord;
    }

    private async Task<IReadOnlyList<SikhnetNewsRecord>> xmlToObject(string xml, CancellationToken cancellationToken)
    {
        var document = XDocument.Parse(xml);
        var items = document.Descendants("item")
            .Select(async item => new SikhnetNewsRecord
            {
                Title = (string?)item.Element("title") ?? "",
                Description = (string?)item.Element("description") ?? "",
                Url = (string?)item.Element("link") ?? "",
                SikhNetNewsRecordId = int.TryParse(((string?)item.Element("guid")), out int result) ? result : 0,
                PublicationDate = convertToDateTime((string?)item.Element("pubDate") ?? ""),
                Image = await convertImageUrlToByte(((string?)item.Element("enclosure")?.Attribute("url") ?? ""), cancellationToken)
            });

        var records = await Task.WhenAll(items);

        var filteredRecords = records.Where(
            item =>
            (item.Title != "") &&
            (item.SikhNetNewsRecordId != 0) &&
            (item.Description != "") &&
            (item.Url != "") &&
            (item.PublicationDate != new DateTime()) &&
            (item.Image != null))
        .ToList();

        return filteredRecords;
    }

    private static DateTime convertToDateTime(string date)
    {
        DateTime dateVal;
        if (date.Equals(""))
        {
            return new DateTime();
        }
        if(!DateTime.TryParseExact(date, "ddd, dd MMM yyyy HH:mm:ss zzz", CultureInfo.InvariantCulture,DateTimeStyles.None, out dateVal))
        {
            throw new FormatException($"Invalid RSS publication date: '{date}'.");
        }
        return dateVal;
    }

    private async Task<byte[]> convertImageUrlToByte(string Url, CancellationToken cancellationToken)
    {
        if (Url == null || Url.Equals(""))
        {
            throw new InvalidDataException("Url cannot be empty or null, something is wrong");
        }
        return await _httpClient.GetByteArrayAsync(Url, cancellationToken).ConfigureAwait(false);
    }
}



