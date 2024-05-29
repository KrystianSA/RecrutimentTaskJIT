using Microsoft.AspNetCore.Mvc;

    [Route("[action]")]
    [ApiController]
    public class CatsController : Controller
    {
        private List<Visit> visitSeeder(){
            
            var visits = new List<Visit>();

            var visit1 = new Visit(){
                id = 1,
                date = new DateTime(2022, 11, 15, 12, 0, 0),
                personName = "John",
                catName = "Garfield",
                age = 5,
                color = "black",
            };

            var visit2 = new Visit(){
                id = 2,
                date = new DateTime(2022, 11, 15,13, 0, 0),
                personName = "Berry",
                catName = "Garfield",
                age = 7,
                color = "black",
            };

            var visit3 = new Visit(){
                id = 3,
                date = new DateTime(2022, 11, 15,14, 0, 0),
                personName = "John",
                catName = "Garfield",
                age = 2,
                color = "black",
            };

            visits.Add(visit1);
            visits.Add(visit2);
            visits.Add(visit3);

            return visits;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Visit>> GetVisits()
        {
            return Ok(visitSeeder());
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<IEnumerable<Visit>> GetVisitsId([FromRoute] int id)
        {
            var visits = visitSeeder();
            var visit = visits.SingleOrDefault(x => x.id == id);

            return Ok(visit);
        }

        [HttpPost]
        public ActionResult CreateVisit([FromBody] Visit visit)
        {
            var visits = visitSeeder();

            try
            {
                var newVisit = new Visit(){
                id = visit.id,
                date = visit.date,
                personName = visit.personName,
                catName = visit.catName,
                age = visit.age,
                color = visit.color,};
                visits.Add(newVisit);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPut]
        public ActionResult UpdateVisit([FromBody] Visit visit)
        {
            var visits = visitSeeder();
            
            var visitToUpdate = visits.SingleOrDefault(x => x.id == visit.id);

            if(visitToUpdate == null)
            {
                throw new Exception("Visit not found");
            }

            visitToUpdate.date = visit.date;
            visitToUpdate.personName = visit.personName;
            visitToUpdate.catName = visit.catName;
            visitToUpdate.age = visit.age;
            visitToUpdate.color = visit.color;

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteVisit([FromRoute] int id)
        {
            var visits = visitSeeder();
            
            var visitToDelete = visits.SingleOrDefault(x => x.id == id);

            if (visitToDelete == null)
            {
                return NotFound();
            }

            visits.Remove(visitToDelete);

            return NoContent();
        }

        [HttpPost]
        public ActionResult IsVisitAvailable([FromBody] DateTime date)
        {
            var visits = visitSeeder();
            var isBusy = visits.Any(x => x.date == date);

            if(isBusy)
            {
                return Ok("Date is available");
            }

            return BadRequest("Date is not available");
        }
        
    }

