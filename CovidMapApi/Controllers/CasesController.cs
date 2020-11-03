using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CovidMapApi.Data;
using CovidMapApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CovidMapApi.Controllers
{
    [Route("covidMapApi/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Bearer")]
    public class CasesController : ControllerBase
    {
        private readonly CasesApiContext _context;
        public CasesController(CasesApiContext context)
        {
            _context = context;
        }
        // GET: api/<CasesController>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<Country>> GetCountries()
        {
            var countries = _context.Countries.ToList();
            foreach(var country in countries)
            {
                country.TotalCase = _context.VirusCases.Where(v => v.CountryId == country.Id).Sum(v => v.Cases);
                country.TotalRecovered = _context.VirusCases.Where(v => v.CountryId == country.Id).Sum(v => v.Recovered);
                country.TotalDeaths = _context.VirusCases.Where(v => v.CountryId == country.Id).Sum(v => v.Deaths);
            }
            
            
            return Ok(countries.OrderByDescending(c => c.TotalCase));
        }

        [HttpGet]
        [Route("global")]
        [AllowAnonymous]
        public ActionResult<VirusCase[]> GetGlobal()
        {
            VirusCase[] globalCases = new VirusCase[12];
            var allCases = _context.VirusCases;
            for(int i=1;i<=12;i++)
            {
                globalCases[i - 1] = new VirusCase();
                globalCases[i - 1].Cases = _context.VirusCases.Where(v => v.Date.Month == i).Sum(v => v.Cases);
                globalCases[i - 1].Recovered = _context.VirusCases.Where(v => v.Date.Month == i).Sum(v => v.Recovered);
                globalCases[i - 1].Deaths = _context.VirusCases.Where(v => v.Date.Month == i).Sum(v => v.Deaths);
            }

            return Ok(globalCases);
        }

        // GET api/<CasesController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<List<VirusCase>> GetCasesByCountryId(int id)
        {
            var cases = _context.VirusCases.Where(v => v.CountryId == id).ToList();
            return Ok(cases);
        }

        // POST api/<CasesController>
        [HttpPost]
        public async Task<ActionResult> AddNewCase([FromBody] VirusCase virusCase)
        {
            if(ModelState.IsValid)
            {
                virusCase.Date = virusCase.Date.ToLocalTime();
                var result = await _context.VirusCases.FirstOrDefaultAsync(v => (DateTime.Compare(v.Date.Date, virusCase.Date.Date) == 0) &&
                                            (v.CountryId==virusCase.CountryId));
                if (result==null)
                {
                    await _context.VirusCases.AddAsync(virusCase);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        // PUT api/<CasesController>/5
        [HttpPut]
        public ActionResult UpdateCase([FromBody] VirusCase virusCase)
        {
            if (ModelState.IsValid)
            {
                virusCase.Date = virusCase.Date.ToLocalTime();
                var result = _context.VirusCases.Where(v => (DateTime.Compare(v.Date.Date, virusCase.Date.Date) == 0) && 
                                        (v.CountryId==virusCase.CountryId)).Any();
                if (result)
                {
                    _context.Entry(virusCase).State = EntityState.Modified;
                    _context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

    }
}
