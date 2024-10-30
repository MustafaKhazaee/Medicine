
using Microsoft.EntityFrameworkCore;

namespace Medicine.Application.Common;

public record Page<T>
{
    private Page() { }

    public List<T> Data { get; private init; }
    public int PageSize { get; private init; }
    public int PageNumber { get; private init; }
    public int TotalPages { get; private init; }
    public int TotalRecords { get; private init; }
    public bool HasNextPage { get; private init; }
    public bool HasPreviousPage { get; private init; }

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