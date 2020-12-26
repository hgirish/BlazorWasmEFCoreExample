﻿using ContactsApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp.Controls.Grid
{
    public class ContactFilters : IContactFilters
    {
        /// <summary>
        /// Default: take scoped instance of page helper
        /// </summary>
        /// <param name="pageHelper">The <see cref="IPageHelper"/> instance.</param>
        public ContactFilters(IPageHelper pageHelper)
        {
            PageHelper = pageHelper;
        }

        /// <summary>
        /// Column filtered text is against.
        /// </summary>
        public ContactFilterColumns FilterColumn { get; set; } = ContactFilterColumns.Name;


        /// <summary>
        /// Text to filter on.
        /// </summary>
        public string FilterText { get; set; }

        /// <summary>
        /// Keep state of paging.
        /// </summary
        public IPageHelper PageHelper { get; set; }

        /// <summary>
        /// Avoid multiple concurrent requests.
        /// </summary>
        public bool Loading { get; set; }

        /// <summary>
        /// Firstname Lastname, or Lastname, Firstname.
        /// </summary>
        public bool ShowFirstNameFirst { get; set; }

        /// <summary>
        /// True when sorting ascending, otherwise sort descending.
        /// </summary>
        public bool SortAscending { get; set; }

        /// <summary>
        /// Column to sort by.
        /// </summary>
        public ContactFilterColumns SortColumn { get; set; } = ContactFilterColumns.Name;
    }
}
