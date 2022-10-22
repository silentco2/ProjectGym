using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectGym.data;

namespace ProjectGym.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachController : ControllerBase
    {
        public readonly AppDBContext db;
        public CoachController(AppDBContext context)
        {
            db = context;
        }
        public static List<Coach> coaches = new List<Coach> { 
            new Coach
            {
                Id = 1,
                FirstName = "zoz",
                LastName = "elsadny",
                Phone = "01061234543"
            },
            new Coach
            {
                Id = 2,
                FirstName = "omar",
                LastName = "samahi",
                Phone = "01551234543"
            },
            new Coach
            {
                Id = 3,
                FirstName = "osama",
                LastName = "khalifa",
                Phone = "01551243645"
            }
            };
        [HttpGet("All")]
        public async Task<ActionResult<List<Coach>>> Get()
        {
            return Ok(await db.Coaches.ToListAsync());
        }
        [HttpPost("SignUp")]
        public async Task<ActionResult<List<Coach>>> SignUp(Coach coach)
        {
            var NewCoach = await db.Coaches.FirstOrDefaultAsync(c => c.Phone == coach.Phone);
            if (NewCoach != null)return BadRequest("user already exist");
            await db.Coaches.AddAsync(coach);
            await db.SaveChangesAsync();
            return Ok(await db.Coaches.ToListAsync());
        }
        [HttpGet("Login")]
        public async Task<ActionResult<Coach>> Login(string phone, string password)
        {
            var coach = await db.Coaches.FirstOrDefaultAsync(c => c.Phone == phone);
            if (coach == null) return BadRequest("The account doesn't exist");
            if (coach.Password != password) return BadRequest("The password is incorrect");
            return Ok(coach);
        }
        [HttpPut("{id}/Password")]
        public async Task<ActionResult<List<Coach>>> UpdatePassword(int id,string password)
        {
            var coach = await db.Coaches.FindAsync(id);
            if (coach == null) return BadRequest("The account doesn't exist");
            coach.Password = password;
            db.Coaches.Update(coach);
            await db.SaveChangesAsync();
            return Ok(await db.Coaches.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Coach>>> Delete(int id)
        {
            var coach = await db.Coaches.FindAsync(id);
            if (coach == null) return BadRequest("The account doesn't exist");
            db.Coaches.Remove(coach);
            await db.SaveChangesAsync();
            return Ok(await db.Coaches.ToListAsync());
        }
        /*[HttpPut("{phone}")]
        public async Task<ActionResult<List<Coach>>> ForgotPassword(string phone, string password)
        {
            var coach = coaches.Find(c => c.Phone == phone);
            if (coach == null) return BadRequest("The account doesn't exist");
            coach.Password = password;
            return Ok(coaches);
        }*/
    }
}
