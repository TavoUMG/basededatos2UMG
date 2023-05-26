using Microsoft.AspNetCore.Mvc;
using static Sistema.Models.View.ModelSweetAlert;

namespace Sistema.Controllers
{
    public class NotificadorController : Controller
    {
        public void AlertSuperior(string message, NotificationType notificationType)
        {
            message = message.Replace("/", "").Replace("'", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
            TempData["notification"] = "<script language='javascript'>" +
                "Swal.fire({position: 'top-end', icon: '" + notificationType.ToString() + "', title: '" + message.Replace("'", "") + "', showConfirmButton: false, timer: 3500})" +
            "</script>";
        }

        public void Alert(string title, string message, NotificationType notificationType)
        {
            message = message.Replace("/", "").Replace("'", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
            TempData["notification"] = "<script language='javascript'>" +
                "Swal.fire({icon: '" + notificationType.ToString() + "', title: '" + title + "', text: '" + message.Replace("'", "") + "'})" +
                "</script>";
        }

        public void AlertHtml(string title, string message, NotificationType notificationType)
        {
            message = message.Replace("/", "").Replace("'", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
            TempData["notification"] = "<script language='javascript'>" +
                "Swal.fire({icon: '" + notificationType.ToString() + "', title: '" + title + "', html: '" + message + "'})" +
                "</script>";
        }

        public void ErrorDataAnnotations(string html)
        {
            TempData["notification"] = "<script language='javascript'>" +
                "Swal.fire({ title: '¡Validaciones!', icon: 'info', html: '" + html + "'," +
                "confirmButtonText: 'Cerrar', confirmButtonColor: '#d33', allowOutsideClick: () => { " +
                "const popup = Swal.getPopup() " +
                "popup.classList.remove('swal2-show') " +
                "setTimeout(() => { popup.classList.add('animate__animated', 'animate__headShake') }) " +
                "setTimeout(() => { popup.classList.remove('animate__animated', 'animate__headShake') }, 500) " +
                "return false } })" +
                "</script>";
        }
    }
}
