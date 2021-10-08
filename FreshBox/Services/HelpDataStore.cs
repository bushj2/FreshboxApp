using System;
using System.Collections.Generic;
using System.Linq;
using FreshBox.Models;



namespace FreshBox.Services
{
    class HelpDataStore
    {
        public static HelpItems HelpTopics { get; } = new HelpItems()
           {
               new HelpItem{Id = "Id1", Text = "Why can I not use the app?", Description = "Im not sure why you can not use the app" },
               new HelpItem{Id = "Id2", Text = "Why can I not login?", Description = "Im not sure why you can not login" }
           };
        public static IEnumerable<HelpItem> GetSearchResults(string queryString)
        {
            var normalizedQuery = queryString?.ToLower() ?? "";
            IEnumerable<HelpItem> Resultset = HelpTopics.Where(f => f.Text.ToLower().Contains(normalizedQuery)).Select(f=>f);
          
            return Resultset;
        }

    }
}
