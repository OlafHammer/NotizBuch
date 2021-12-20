using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotizBuch.Controllers.Interfaces
{
    interface IControllerBase<T>
    {

        IActionResult GetAll();

        IActionResult Get(int id);

        IActionResult Insert(T t);

        IActionResult Edit(T t);

        IActionResult Delete(int id);

    }
}
