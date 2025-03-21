using Application.Books.Commands.Create;
using Application.Books.Commands.Delete;
using Application.Books.Commands.Update;
using Application.Books.Queries.GetAll;
using Application.Books.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectire.Controllers;

[ApiController]
[Route("api/[controller]")]

public class BooksController : Controller
{
    private readonly IMediator _mediator;

    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }   
    
    [HttpGet]
    public async Task<ActionResult<List<BookListDto>>> GetAll()
    {
        var books = await _mediator.Send(new GetAllBooksQuery());
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookDetailsDto>> GetById([FromRoute] int id)
    {
        var book = await _mediator.Send(new GetByIdBookQuery { Id = id });
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