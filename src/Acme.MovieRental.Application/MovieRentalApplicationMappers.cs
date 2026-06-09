using Acme.MovieRental.Customers;
using Acme.MovieRental.Directors;
using Acme.MovieRental.Movies;
using Acme.MovieRental.Rentals;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace Acme.MovieRental;

[Mapper]
public partial class MovieToMovieDtoMapper : MapperBase<Movie, MovieDto>
{
    public override MovieDto Map(Movie source)
    {
        return new MovieDto
        {
            Id = source.Id,
            Title = source.Title,
            Genre = source.Genre,
            YearOfRelease = source.YearOfRelease,
            Price = source.Price,
            DirectorId = source.DirectorId,
            DirectorName = source.Director != null ? source.Director.Name : null
        };
    }

    public override void Map(Movie source, MovieDto destination)
    {
        destination.Id = source.Id;
        destination.Title = source.Title;
        destination.Genre = source.Genre;
        destination.YearOfRelease = source.YearOfRelease;
        destination.Price = source.Price;
        destination.DirectorId = source.DirectorId;
        destination.DirectorName = source.Director != null ? source.Director.Name : null;
    }
}

[Mapper]
public partial class CreateUpdateMovieDtoToMovieMapper : MapperBase<CreateUpdateMovieDto, Movie>
{
    public override Movie Map(CreateUpdateMovieDto source)
    {
        return new Movie
        {
            Title = source.Title,
            Genre = source.Genre,
            YearOfRelease = source.YearOfRelease,
            Price = source.Price,
            DirectorId = source.DirectorId
        };
    }

    public override void Map(CreateUpdateMovieDto source, Movie destination)
    {
        destination.Title = source.Title;
        destination.Genre = source.Genre;
        destination.YearOfRelease = source.YearOfRelease;
        destination.Price = source.Price;
        destination.DirectorId = source.DirectorId;
    }
}

[Mapper]
public partial class DirectorToDirectorDtoMapper : MapperBase<Director, DirectorDto>
{
    public override DirectorDto Map(Director source)
    {
        return new DirectorDto
        {
            Id = source.Id,
            Name = source.Name,
            Nationality = source.Nationality
        };
    }

    public override void Map(Director source, DirectorDto destination)
    {
        destination.Id = source.Id;
        destination.Name = source.Name;
        destination.Nationality = source.Nationality;
    }
}

[Mapper]
public partial class CreateUpdateDirectorDtoToDirectorMapper : MapperBase<CreateUpdateDirectorDto, Director>
{
    public override Director Map(CreateUpdateDirectorDto source)
    {
        return new Director
        {
            Name = source.Name,
            Nationality = source.Nationality
        };
    }

    public override void Map(CreateUpdateDirectorDto source, Director destination)
    {
        destination.Name = source.Name;
        destination.Nationality = source.Nationality;
    }
}

[Mapper]
public partial class CustomerToCustomerDtoMapper : MapperBase<Customer, CustomerDto>
{
    public override CustomerDto Map(Customer source)
    {
        return new CustomerDto
        {
            Id = source.Id,
            FullName = source.FullName,
            Email = source.Email,
            PhoneNumber = source.PhoneNumber
        };
    }

    public override void Map(Customer source, CustomerDto destination)
    {
        destination.Id = source.Id;
        destination.FullName = source.FullName;
        destination.Email = source.Email;
        destination.PhoneNumber = source.PhoneNumber;
    }
}

[Mapper]
public partial class CreateUpdateCustomerDtoToCustomerMapper : MapperBase<CreateUpdateCustomerDto, Customer>
{
    public override Customer Map(CreateUpdateCustomerDto source)
    {
        return new Customer
        {
            FullName = source.FullName,
            Email = source.Email,
            PhoneNumber = source.PhoneNumber
        };
    }

    public override void Map(CreateUpdateCustomerDto source, Customer destination)
    {
        destination.FullName = source.FullName;
        destination.Email = source.Email;
        destination.PhoneNumber = source.PhoneNumber;
    }
}

[Mapper]
public partial class RentalToRentalDtoMapper : MapperBase<Rental, RentalDto>
{
    public override RentalDto Map(Rental source)
    {
        return new RentalDto
        {
            Id = source.Id,
            CreationTime = source.CreationTime,
            CreatorId = source.CreatorId,
            LastModificationTime = source.LastModificationTime,
            LastModifierId = source.LastModifierId,
            CustomerId = source.CustomerId,
            CustomerName = source.Customer != null ? source.Customer.FullName : null,
            MovieId = source.MovieId,
            MovieTitle = source.Movie != null ? source.Movie.Title : null,
            RentalDate = source.RentalDate,
            DueDate = source.DueDate,
            ReturnDate = source.ReturnDate,
            IsReturned = source.IsReturned
        };
    }

    public override void Map(Rental source, RentalDto destination)
    {
        destination.Id = source.Id;
        destination.CreationTime = source.CreationTime;
        destination.CreatorId = source.CreatorId;
        destination.LastModificationTime = source.LastModificationTime;
        destination.LastModifierId = source.LastModifierId;
        destination.CustomerId = source.CustomerId;
        destination.CustomerName = source.Customer != null ? source.Customer.FullName : null;
        destination.MovieId = source.MovieId;
        destination.MovieTitle = source.Movie != null ? source.Movie.Title : null;
        destination.RentalDate = source.RentalDate;
        destination.DueDate = source.DueDate;
        destination.ReturnDate = source.ReturnDate;
        destination.IsReturned = source.IsReturned;
    }
}