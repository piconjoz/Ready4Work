import { useEffect, useState } from "react";
import { getCompanies } from "../../../services/companiesApi.js";

function CompaniesPage() {
  const [companies, setCompanies] = useState([]);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    const fetchCompanies = async () => {
      setLoading(true);
      try {
        const data = await getCompanies();
        setCompanies(data);
        console.log("got companies:", data);
      } catch (error) {
        console.log("error:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchCompanies();
  }, []);

  if (loading) return <div>loading...</div>;

  return (
    <div>
      <h1>Companies Page</h1>
      <p>found {companies.length} companies</p>

      {/* super basic display */}
      {companies.map((company) => (
        <div key={company.companyId}>
          <h3>{company.name}</h3>
          <p>{company.description}</p>
        </div>
      ))}
    </div>
  );
}

export default CompaniesPage;
