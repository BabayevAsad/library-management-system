﻿using MediatR;

namespace Application.Library.Queries.GetAll;

public class GetAllLibrariesQuery : IRequest<List<LibraryListDto>> 
{
}