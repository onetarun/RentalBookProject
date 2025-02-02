using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookRent.Domain.Entities;
using BookRent.Application.Interfaces;

namespace BookRent.Application.Interfaces.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepo Book { get; }
        IGenreRepository Genre { get; }



    }
}
