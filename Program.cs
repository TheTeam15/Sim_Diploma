using GestaoCursosMVC.Controller;
using GestaoCursosMVC.Model;
using GestaoCursosMVC.View;

namespace GestaoCursosMVC
{
    internal class Program
    {
        private static void Main()
        {
            GestaoCursos model = new GestaoCursos();
            GestaoCursosView view = new GestaoCursosView();
            GestaoCursosController controller = new GestaoCursosController(model, view);

            controller.Iniciar();
        }
    }
}
