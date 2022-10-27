using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Social_Brothers_case.Data;
using Social_Brothers_case.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static NuGet.Client.ManagedCodeConventions;

namespace Social_Brothers_case.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class adressesController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();
        public static object ReflectPropertyValue(object source, string property)
        {
            return source.GetType().GetProperty(property).GetValue(source, null);
        }

        private readonly Social_Brothers_caseContext _context;

        public adressesController(Social_Brothers_caseContext context)
        {
            _context = context;
        }

        // GET: api/adresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<adress>>> Getadress()
        {
            return await _context.adress.ToListAsync();
        }


        //get with filter
        [HttpGet]
        [Route("Filters")]
        public async Task<ActionResult<IEnumerable<adress>>> GetAdressesWithFilter(string filter,bool desc,string? atribute)
        {
            //get all
            var getAll = await _context.adress
                .ToListAsync();

            var result = new List<adress>();

             //string prop;
            var propertyInfo = typeof(adress).GetProperty(atribute);
            PropertyInfo[] properties = typeof(adress).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                //get property of type adress
                result.AddRange(getAll.FindAll(delegate (adress a)
                {
                    return ReflectPropertyValue(a, property.Name).ToString()
                    .Contains(filter);
                }));

            }

            if (desc)
            {
                return result.Distinct()
                    .OrderByDescending(x => propertyInfo.GetValue(x, null)).ToList();

            }
            else if (desc==false)
            {
                return result.Distinct()
                    .OrderBy(x => propertyInfo.GetValue(x, null)).ToList();
            }

            return result.Distinct()
                .OrderBy(x=>propertyInfo.GetValue(x, null)).ToList();
            
        }

        // GET: api/adresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<adress>> Getadress(int id)
        {
            var adress = await _context.adress.FindAsync(id);

            if (adress == null)
            {
                return NotFound();
            }

            return adress;
        }

        // PUT: api/adresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putadress(int id, adress adress)
        {
            if (id != adress.id)
            {
                return BadRequest();
            }

            _context.Entry(adress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!adressExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/adresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<adress>> Postadress(adress adress)
        {
            _context.adress.Add(adress);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getadress", new { id = adress.id }, adress);
        }

        // DELETE: api/adresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteadress(int id)
        {
            var adress = await _context.adress.FindAsync(id);
            if (adress == null)
            {
                return NotFound();
            }

            _context.adress.Remove(adress);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool adressExists(int id)
        {
            return _context.adress.Any(e => e.id == id);
        }


        // GET: api/adresses
        [HttpGet]
        [Route("distance")]
        public async Task<ActionResult<object>> getDistance(int id1,int id2)
        {
            var adress1 = await _context.adress.FindAsync(id1);
            var adress2 = await _context.adress.FindAsync(id2);

            var adressStr1 = adress1.zipCode + " " + adress1.street + ", " + adress1.city + ", " + adress1.country;
            var adressStr2 = adress2.zipCode + " " + adress2.street + ", " + adress2.city + ", " + adress2.country;

            var responseString = await client.GetStringAsync("http://api.positionstack.com/v1/forward?access_key=d58e53f0afda76cceeec3c78f574bf66&limit=1&query=" + adressStr1);
            var responseString2 = await client.GetStringAsync("http://api.positionstack.com/v1/forward?access_key=d58e53f0afda76cceeec3c78f574bf66&limit=1&query=" + adressStr2);

            dynamic m = JsonConvert.DeserializeObject<Root>(responseString);
            dynamic m2 = JsonConvert.DeserializeObject<Root>(responseString2);
            Console.WriteLine(m.data[0].latitude);

            return Calculate(m.data[0].latitude, m.data[0].longitude, m2.data[0].latitude, m2.data[0].longitude);
        }

        public static double Calculate(double sLatitude, double sLongitude, double eLatitude,
                               double eLongitude)
        {
            var radiansOverDegrees = (Math.PI / 180.0);

            var sLatitudeRadians = sLatitude * radiansOverDegrees;
            var sLongitudeRadians = sLongitude * radiansOverDegrees;
            var eLatitudeRadians = eLatitude * radiansOverDegrees;
            var eLongitudeRadians = eLongitude * radiansOverDegrees;

            var dLongitude = eLongitudeRadians - sLongitudeRadians;
            var dLatitude = eLatitudeRadians - sLatitudeRadians;

            var result1 = Math.Pow(Math.Sin(dLatitude / 2.0), 2.0) +
                          Math.Cos(sLatitudeRadians) * Math.Cos(eLatitudeRadians) *
                          Math.Pow(Math.Sin(dLongitude / 2.0), 2.0);

            // Using 3956 as the number of miles around the earth
            var result2 = 3956.0 * 2.0 *
                          Math.Atan2(Math.Sqrt(result1), Math.Sqrt(1.0 - result1));

            return result2;
        }

    }
}
