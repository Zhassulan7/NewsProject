using Models.Abstract;
using Models.DTO;
using NewsParser;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class InformWrite : INewsWrite
    {
        public void Write()
        {
            using (var context = new NewsDbContext())
            {
                context.News.AddRange(new InformParse(@"https://lenta.inform.kz",
                                    @"https://lenta.inform.kz/ru/archive/?date=")
                                    .GetParsedData());
                context.SaveChanges();
            }
        }
    }
}
