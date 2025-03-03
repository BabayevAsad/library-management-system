using Api.Books;
using Application.Application.Caching;
using Application.Books.Commands.Create;
using Application.Books.Commands.Delete;
using Application.Books.Commands.Update;
using Application.Books.Queries.GetAll;
using Application.Books.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitectire.Controllers;

[ApiController]
[Route("api/[controller]")]

public class BooksController : Controller
{
    private readonly IDistributedCache _cache;
    private readonly IMediator _mediator;

    public BooksController(IDistributedCache cache, IMediator mediator)
    {
        _cache = cache;
        _mediator = mediator;
    }   
    
    [HttpGet]
    public async Task<ActionResult<List<Book>>> GetAll()
    {
        var books = await _mediator.Send(new GetAllBooksQuery());
        
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetById([FromRoute] int id)
    {
        string key = $"book-{id}";

        var book = await _cache.GetOrCreateAsync(key, async token =>
        {
            var book = await _mediator.Send(new GetByIdBookQuery { Id = id });
            return book;
        });
        
        return Ok(book);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateBookCommand command)
    {
        var bookId = await _mediator.Send(command);

        return Created("", bookId);
    }

    [HttpPut("{id}")]
    public async Task<NoContentResult> Update([FromRoute] int id, [FromBody] UpdateBookCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        await _mediator.Send(new DeleteBookCommand()
        {
            Id = id
        });

        return NoContent();
    }
}