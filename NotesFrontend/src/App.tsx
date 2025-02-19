import { useState, useEffect } from "react";
import axios from "axios";

interface UserType {
  id: string;
  name: string;
  email: string;
  profilePicUrl: string | null;
}

const App = () => {
  const [users, setUsers] = useState<UserType[]>([]);
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    axios
      .get("https://localhost:7060/api/users")
      .then((response) => {
        setUsers(response.data);
      })
      .catch((error) => console.error("Error:", error))
      .finally(() => setLoading(false));
  }, []);

  return (
    <div>
      {loading ? (
        <p>Loading...</p>
      ) : (
        <ul>
          {users.map((user) => (
            <li key={user.id}>
              <h3>{user.name}</h3>
              <h3>{user.email}</h3>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
};

export default App;
