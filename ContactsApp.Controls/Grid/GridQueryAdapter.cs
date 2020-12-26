using ContactsApp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp.Controls.Grid
{
    /// <summary>
    /// Creates the right expressions to filter and sort
    /// </summary>
    public class GridQueryAdapter
    {
        /// <summary>
        /// Holds state of the grid
        /// </summary>
        private readonly IContactFilters _controls;

        /// <summary>
        /// Expression for sorting
        /// </summary>
        private readonly Dictionary<ContactFilterColumns,
            Expression<Func<Contact, string>>> _expressions =
            new Dictionary<ContactFilterColumns, Expression<Func<Contact, string>>>
            {
                {ContactFilterColumns.City, c=>c.City },
                {ContactFilterColumns.Phone, c => c.Phone },
                {ContactFilterColumns.Name, c=>c.LastName },
                {ContactFilterColumns.State, c=> c.State },
                {ContactFilterColumns.Street, c => c.Street },
                {ContactFilterColumns.ZipCode, c=> c.ZipCode }
            };

        /// <summary>
        /// Queryable for filtering
        /// </summary>
        private readonly Dictionary<ContactFilterColumns,
            Func<IQueryable<Contact>, IQueryable<Contact>>> _filterQueries;

        /// <summary>
        /// Creates a new instance of the <see cref="GridQueryAdapter"/> class.
        /// </summary>
        /// <param name="controls">The <see cref="IContactFilters"/> to use.</param>
        public GridQueryAdapter(IContactFilters controls)
        {
            _controls = controls;

            _filterQueries = new Dictionary<ContactFilterColumns, 
                Func<IQueryable<Contact>, IQueryable<Contact>>>
            {
                {ContactFilterColumns.City, cs => cs.Where(
                    c=>c.City.Contains(_controls.FilterText)) },
                {ContactFilterColumns.Phone, cs => cs.Where(
                    c=>c.Phone.Contains(_controls.FilterText)) },
                {ContactFilterColumns.Name, cs => cs.Where(
                    c=>c.FirstName.Contains(_controls.FilterText) || 
                    c.LastName.Contains(_controls.FilterText)) },
                {ContactFilterColumns.State, cs => cs.Where(
                    c=>c.State.Contains(_controls.FilterText)) },
                {ContactFilterColumns.Street, cs => cs.Where(
                    c=>c.Street.Contains(_controls.FilterText)) },
                {ContactFilterColumns.ZipCode, cs => cs.Where(
                    c=>c.ZipCode.Contains(_controls.FilterText)) },
            };
        }

        /// <summary>
        /// Uses the query to return a count and a page
        /// </summary>
        /// <param name="query">The <<see cref="IQueryable{Contact}"/> to work from.</param>
        /// <returns>The resulting <see cref="IQueryable{Contact}"/>.</returns>
        public async Task<ICollection<Contact>> FetchAsync(IQueryable<Contact> query)
        {
            query = FilterAndQuery(query);
            await CountAsync(query);
            var collection = await FetchPageQuery(query)
                .ToListAsync();
            _controls.PageHelper.PageItems = collection.Count;
            return collection;
        }

        /// <summary>
        /// Get total filtered items count.
        /// </summary>
        /// <param name="query">The <see cref="IQueryable{Contact}"/> to use.</param>
        /// <returns>Asynchronous <see cref="Task"/>.</returns>
        private async Task CountAsync(IQueryable<Contact> query)
        {
            _controls.PageHelper.TotalItemCount = await query.CountAsync();
        }
    }
}
