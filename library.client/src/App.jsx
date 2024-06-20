import { useEffect, useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Toaster, toast } from 'react-hot-toast';

function App() {
    const [libros, setLibros] = useState([]);
    const [nombreLib, setNombreLib] = useState("");
    const [autorLib, setAutorLib] = useState("");
    const [copiasLib, setCopiasLib] = useState("");

    const mostrarLibros = async () => {

        const response = await fetch("api/book/List");

        if (response.ok) {
            const data = await response.json();
            console.log(data)
            setLibros(data)
        } else {
            console.log("status code:" + response.status)
        }
    }

    useEffect(() => {
        mostrarLibros();
    }, [])


    const guardarLibro = async (e) => {

        e.preventDefault()

        const response = await fetch("api/book/Insert", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify({ Title: nombreLib, Author: autorLib, Copies: copiasLib })
        })

        if (response.ok) {
            setNombreLib("");
            setAutorLib("");
            setCopiasLib("");
            await mostrarLibros();

            toast.success("El libro se registro correctamente");
        }
        else
            toast.error("Error al registrar el libro");
    }

    const eliminarLibro = async (id) => {

        const response = await fetch("api/book/Remove/" + id, {
            method: "DELETE"
        })

        if (response.ok) {
            toast.success("El libro se elimino correctamente");
            await mostrarLibros();
        }
        else {
            toast.error("El libro no pudo ser eliminado");
        }
            
    }

    const prestarLibro = async (id) => {

        const response = await fetch("api/book/Lent/" + id, {
            method: "Get"
        })

        if (response.ok) {
            toast.success("El libro fue prestado correctamente");
            await mostrarLibros();
        }
        else {
            toast.error("El libro no tiene copias que puedan ser prestadas");
        }
    }

    const devolverLibro = async (id) => {

        const response = await fetch("api/book/Return/" + id, {
            method: "Get"
        })

        if (response.ok) {
            toast.success("Se ha devuelto y/o agregado el libro correctamente");
            await mostrarLibros();
        }
        else{
            toast.error("Error el libro no pudo ser devuelto");
        }
    }

    return (
        <>
        <div className="container bg-dark p-6 vh-100 w-75">
            <h2 className="text-white">Lista de Libros</h2>
            <div className="row">
                <div className="col-sm-12">
                    <form onSubmit={guardarLibro}>

                        <div className="input-group">
                            <input type="text" className="form-control"
                                placeholder="Nombre del libro"
                                value={nombreLib}
                                onChange={(e) => setNombreLib(e.target.value)} />
                            <input type="text" className="form-control"
                                placeholder="Autor"
                                value={autorLib}
                                onChange={(e) => setAutorLib(e.target.value)} />
                            <input type="number" min="1" className="form-control"
                                placeholder="No. Copias"
                                value={copiasLib}
                                onChange={(e) => setCopiasLib(e.target.value)} />
                            <button className="btn btn-success">Agregar</button>
                        </div>
                    </form>
                </div>
            </div>

            <div className="row mt-4">
                <div className="col-sm-12">
                    <div className="list-group">
                        {
                            libros.map(
                                (item) => (
                                    <div key={item.id} className="list-group-item list-group-item-action">

                                        <h5 className="text-primary">{item.title}</h5>

                                        <div className="d-flex justify-content-between">
                                            <small className="text-muted">{item.author}</small>
                                            <small className="text-muted">Copias disponibles: {item.copies}</small>
                                            <button type="button" className="btn btn-sm btn-outline-danger"
                                                onClick={() => eliminarLibro(item.id)}>
                                                Eliminar
                                            </button>
                                            <button type="button" className="btn btn-sm btn-outline-primary"
                                                onClick={() => prestarLibro(item.id)}>
                                                Prestar
                                            </button>
                                            <button type="button" className="btn btn-sm btn-outline-primary"
                                                onClick={() => devolverLibro(item.id)}>
                                                Devolver/Agregar
                                            </button>
                                        </div>

                                    </div>
                                )
                            )
                        }
                    </div>
                </div>
            </div>
        </div>
        <Toaster position="top-right" reverseOrder={false} />
        </>
    )
}
export default App;