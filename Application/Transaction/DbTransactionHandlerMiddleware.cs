using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Transaction;

public class DbTransactionHandlerMiddleware<TDbContext> where TDbContext : DbContext
{
    private readonly RequestDelegate _next;

    public DbTransactionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, TDbContext dbContext)
    {
        if (context.Request.Method.Equals("GET", StringComparison.CurrentCultureIgnoreCase))
        {
            await _next(context);
        }
        else
        {
            await using var transaction = await dbContext.Database.BeginTransactionAsync();
            await _next(context);
            await transaction.CommitAsync();
        }
        
    }
    
}