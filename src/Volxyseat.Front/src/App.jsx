import './App.css';
import { useEffect, useState } from 'react';
import axios from 'axios';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const apiUrl = "https://localhost:7198/api/clients";

function App() {
  const [clients, setClients] = useState([]);
  const [client, setClient] = useState({ name: "", cpf: "", email: "", phone: "" });
  const [isEditing, setIsEditing] = useState(false);
  const [editingClientId, setEditingClientId] = useState(null);

  useEffect(() => {
    async function fetchClients() {
      try {
        const response = await axios.get(apiUrl);
        setClients(response.data);
      } catch (error) {
        console.error("Erro ao buscar clientes:", error);
      }
    }
    fetchClients();
  }, []);

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setClient({ ...client, [name]: value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      if (isEditing) {
        //put
        await axios.put(`${apiUrl}/${editingClientId}`, client);
        toast.success("Cliente editado com sucesso!");
      } else {
        //post
        await axios.post(apiUrl, client);
        toast.success("Cliente cadastrado com sucesso!");
      }

      const response = await axios.get(apiUrl);
      setClients(response.data);

      // Limpe os campos de entrada
      setClient({ name: "", cpf: "", email: "", phone: "" });

      setIsEditing(false);
      setEditingClientId(null);
    } catch (error) {
      console.error("Erro ao cadastrar cliente:", error);
      toast.error("Erro ao cadastrar cliente.");
    }
  };

  const handleEdit = (id) => {
    const clientToEdit = clients.find((c) => c.id === id);
    if (clientToEdit) {
      setClient({ ...clientToEdit });
      setIsEditing(true);
      setEditingClientId(id);
    }
  };

  const handleDelete = async (id) => {
    try {
      await axios.delete(`${apiUrl}/${id}`);
      const updatedClients = clients.filter((c) => c.id !== id);
      setClients(updatedClients);
      toast.success("Cliente excluído com sucesso!");
    } catch (error) {
      console.error("Erro ao excluir cliente:", error);
      toast.error("Erro ao excluir cliente.");
    }
  };

  return (
    <>
      <div className="client-container">
        <h1>{isEditing ? "Editar Cliente" : "Cadastro de Cliente"}</h1>
        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label htmlFor="Name">Nome:</label>
            <input type="text" id="Name" name="name" value={client.name} onChange={handleInputChange} required />
          </div>
          <div className="form-group">
            <label htmlFor="Email">Email:</label>
            <input type="email" id="Email" name="email" value={client.email} onChange={handleInputChange} required />
          </div>
          <div className="form-group">
            <label htmlFor="Cpf">Cpf:</label>
            <input type="text" id="Cpf" name="cpf" value={client.cpf} onChange={handleInputChange} required />
          </div>
          <div className="form-group">
            <label htmlFor="Phone">Telefone:</label>
            <input type="text" id="Phone" name="phone" value={client.phone} onChange={handleInputChange} required />
          </div>
          <button type="submit" className="cta-button">
            {isEditing ? "Salvar Edição" : "Cadastrar Cliente"}
          </button>
        </form>
        <h2>Lista de Clientes</h2>
        {clients.length > 0 ? (
          <table className="client-table">
            <thead>
              <tr>
                <th>ID</th>
                <th>Nome</th>
                <th>Cpf</th>
                <th>Email</th>
                <th>Telefone</th>
                <th>Ações</th>
              </tr>
            </thead>
            <tbody>
              {clients.map((c) => (
                <tr key={c.id}>
                  <td>{c.id}</td>
                  <td>{c.name}</td>
                  <td>{c.cpf}</td>
                  <td>{c.email}</td>
                  <td>{c.phone}</td>
                  <td className="cta-buttons">
                    <button className="cta-edit" onClick={() => handleEdit(c.id)}>
                      Editar
                    </button>
                    <button className="cta-delete" onClick={() => handleDelete(c.id)}>
                      Excluir
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        ) : (
          <p>Nenhum cliente encontrado.</p>
        )}
      </div>
      <ToastContainer />
    </>
  );
}

export default App;
