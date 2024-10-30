
using Microsoft.EntityFrameworkCore;

namespace Medicine.Application.Common;

public class Page<T>
{
    private Page() { }

    public List<T> Data { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
    public bool HasNextPage { get; set; }
    public bool HasPreviousPage { get; set; }

    public async static Task<Page<T>> CreateAsync(IQueryable<T> queryable, int pageSize, int pageNumber, CancellationToken cancellationToken = default)
    {
        if (pageNumber < 1)
            throw new Exception("PageNumber should be a positive number!");

        if (pageSize < 1)
            throw new Exception("PageSize should be a positive number!");

        var totalRecords = await queryable.CountAsync(cancellationToken);

        var totalPages = (int)Math.Ceiling(((decimal)totalRecords / pageSize));

        if (totalPages < pageNumber)
            throw new Exception("PageNumber cannot be larger than totalPages");

        var skip = (int)Math.Ceiling((decimal)(pageSize * (pageNumber - 1)));

        var data = await queryable.Skip(skip).Take(pageSize).ToListAsync(cancellationToken);

        var hasNextPage = totalPages > pageNumber;

        var hasPreviousPage = 1 < pageNumber && 1 < totalPages;

        return new Page<T>
        {
            PageSize = pageSize,
            PageNumber = pageNumber,
            TotalPages = totalPages,
            HasPreviousPage = hasPreviousPage,
            HasNextPage = hasNextPage,
            Data = data,
            TotalRecords = totalRecords
        };
    }
}
