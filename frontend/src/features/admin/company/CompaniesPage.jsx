import { useEffect, useState } from "react";
import { getCompanies } from "../../../services/companiesApi.js";
import ExampleComponent from "./ExampleComponent.jsx";

function CompaniesPage() {
  const [companies, setCompanies] = useState([]);
  const [loading, setLoading] = useState(false);
  const [count, setCount] = useState(0);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchCompanies = async () => {
      setLoading(true);
      setError(null);
      try {
        const data = await getCompanies();
        setCompanies(data);
        console.log("got companies:", data);
      } catch (error) {
        console.log("error:", error);
        setError("failed to load companies. please try again.");
      } finally {
        setLoading(false);
      }
    };

    fetchCompanies();
  }, []);

  // show error state
  if (error) {
    return (
      <div>
        <h1>Companies Page</h1>
        <div style={{ color: "red", padding: "10px", border: "1px solid red" }}>
          {error}
          <button onClick={() => window.location.reload()}>retry</button>
        </div>
      </div>
    );
  }

  if (loading) return <div>loading...</div>;

  return (
    <div>
      <h1>Companies Page</h1>
      <p>found {companies.length} companies</p>

      {/* show message if no companies */}
      {companies.length === 0 ? (
        <p>no companies found</p>
      ) : (
        companies.map((company) => (
          <div key={company.companyId}>
            <h3>{company.name}</h3>
            <p>{company.description}</p>
          </div>
        ))
      )}
    </div>
  );
}

export default CompaniesPage;
