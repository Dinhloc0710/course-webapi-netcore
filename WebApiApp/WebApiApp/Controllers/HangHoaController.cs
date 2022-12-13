using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiApp.Models;

namespace WebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        public static List<HangHoa> hangHoaMang = new List<HangHoa>();


        [HttpGet]
        public IActionResult GetAll() //IActionResult là interface dành cho việc trả về cho các Action nôm na là method 
        {
            return Ok(hangHoaMang);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(string id) //IActionResult là interface dành cho việc trả về cho các Action nôm na là method 
        {
            try
            {
                //Linq [Object] Query
                // SingleOrDefault có thì trả về ko có thì trả về null
                var hangHoa = hangHoaMang.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));

                if (hangHoa == null)
                {
                    return NotFound();
                }
                return Ok(hangHoa);
            }
            catch
            {
                return BadRequest(); // yêu cầu ko hợp lệ 
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(string id)
        {
            try
            {
                //Linq [Object] Query
                // SingleOrDefault có thì trả về ko có thì trả về null
                var hangHoa = hangHoaMang.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));

                if (hangHoa == null)
                {
                    return NotFound();
                }

                //Delete
                hangHoaMang.Remove(hangHoa);
                return Ok();
            }
            catch
            {
                return BadRequest(); // yêu cầu ko hợp lệ 
            }
        }
    

        [HttpPost]
        public IActionResult CreateNew(HangHoaVM hHVM)
        {
            var hanghoamoi = new HangHoa
            {
             MaHangHoa = Guid.NewGuid(),
             TenHangHoa = hHVM.TenHangHoa,
             DonGia = hHVM.DonGia

         };
            hangHoaMang.Add(hanghoamoi);
            return Ok(
            new
            {
                Success = true,
                Data = hanghoamoi
            }
            );
    }
    [HttpPut("{id}")]
    public IActionResult Edit(string id, HangHoa hhEdit)
    {
        try
        {
            //Linq [Object] Query
            // SingleOrDefault có thì trả về ko có thì trả về null
            var hangHoa = hangHoaMang.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));

            if (hangHoa == null)
            {
                return NotFound();
            }
            if (id != hangHoa.MaHangHoa.ToString())
            {
                return BadRequest();
            }
            //Update
            hangHoa.TenHangHoa = hhEdit.TenHangHoa;
            hangHoa.DonGia = hhEdit.DonGia;
            return Ok();
        }
        catch
        {
            return BadRequest(); // yêu cầu ko hợp lệ 
        }
    }

    }
}
