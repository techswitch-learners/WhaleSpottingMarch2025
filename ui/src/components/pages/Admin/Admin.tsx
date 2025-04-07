import { useContext } from "react";
import { LoginContext } from "../../LoginManager/LoginManager";
import { useEffect, useState } from "react";
import { SightingsResponse, Species } from "../../../models/apiModels";
import "./Admin.scss";
import { getAllSpecies, getPendingApprovalS } from "../../../utils/apiClient";

const testPendindApprovals = [
  {
    id: 9,
    species: {
      id: 9,
      speciesName: "Right Whale",
    },
    description: "Details of Sighting 9",
    sightingDate: "2024-03-09T13:21:33Z",
    reportDate: "2024-03-10T13:21:33Z",
    quantity: 1,
    location: {
      id: 9,
      latitude: 44.420668,
      longitude: -56.366203,
    },
    imageSource: "http://localhost:5067/images/blue-whale.jpg",
  },
];

export const Admin = () => {
  const loginContext = useContext(LoginContext);
  const [pendingApprovals, setPendingApprovals] = useState<SightingsResponse[]>(
    [],
  );
  const [pendingApprovalsError, setPendingApprovalsError] = useState(false);
  const [speciesOptions, setSpeciesOptions] = useState<Species[]>([]);
  const [speciesLoadingError, setSpeciesLoadingError] = useState(false);
  const [selectedSpecies, setSelectedSpecies] = useState<Species | null>(null);

  useEffect(() => {
    async function fetchPendingApprovals() {
      // const pendingApprovals = await getPendingApprovalS().catch((error) => {
      //   setPendingApprovalsError(error);
      // });
      // setPendingApprovals(pendingApprovals);
      setPendingApprovals(testPendindApprovals);
    }
    fetchPendingApprovals();
  }, []);

  useEffect(() => {
    async function fetchSpecies() {
      const species = await getAllSpecies().catch((error) => {
        setSpeciesLoadingError(error);
      });

      setSpeciesOptions(species);
    }
    fetchSpecies();
  }, []);

  if (!loginContext.isLoggedIn && !loginContext.isAdmin) {
    return (
      <div>
        <h3> Only admin can view this page. Please log in as admin </h3>
        <h3>
          <a href="/LogIn"> Login </a>
        </h3>
      </div>
    );
  }

  // This part is hardcoded because it hasn't been implemented.
  const renderAdminInfo = () => (
    <div className="admin__info">
      <h2>Hello Whale_spotting!</h2>
      <div>Role: Admin</div>
      <div className="admin__details">
        <div className="admin__password">Edit: Password</div>
        <div className="admin__email">Edit: Email</div>
      </div>
    </div>
  );

  const renderPendingApprovalsTitle = () => {
    const pendingApprovalsCount = pendingApprovals.length;

    return (
      <div className="admin__approvals-title">
        <h2>Pending Approvals</h2>
        {pendingApprovalsCount >= 1 ? (
          <div>
            You have {pendingApprovalsCount} sighting
            {pendingApprovalsCount !== 1 && "s"} to approve!
          </div>
        ) : (
          <div>You have no sightings to approve</div>
        )}
      </div>
    );
  };

  const renderSpeciesOptions = (sighting: SightingsResponse) => (
    <select
      className="inputStyle"
      name="species"
      value={sighting.species.speciesName}
      // onChange={handleChangeSpecies}
      required
    >
      {speciesLoadingError ? (
        <div>Error loading species</div>
      ) : (
        <>
          <option value="">Change species</option>
          {speciesOptions &&
            speciesOptions.map(({ id, speciesName }) => (
              <option key={`admin-page-speciesName-${id}`} value={id}>
                {speciesName}
              </option>
            ))}
        </>
      )}
    </select>
  );

  const renderPendingAppovalsTableHeader = () => (
    <thead className="table-header">
      <th className="table-cell">Id</th>
      <th className="table-cell">Species</th>
      <th className="table-cell">Description</th>
      <th className="table-cell">Approve</th>
      <th className="table-cell">Reject</th>
    </thead>
  );

  const renderPendingApprovalsTableBody = () => (
    <tbody>
      {pendingApprovals?.map((sighting) => (
        <tr className="table-row">
          <td className="table-cell">{sighting.id}</td>
          <td className="table-cell">
            <div>{sighting.species.speciesName}</div>
            <div>{renderSpeciesOptions(sighting)}</div>
          </td>
          <td className="table-cell">{sighting.description}</td>
          <td className="table-cell">
            <button className="admin__approve">Approve</button>
          </td>
          <td className="table-cell">
            <button className="admin__reject">Deny</button>
          </td>
        </tr>
      ))}
    </tbody>
  );

  const renderPendingApprovalsTable = () => {
    return (
      <table id="" className="table-container">
        {renderPendingAppovalsTableHeader()}
        {renderPendingApprovalsTableBody()}
      </table>
    );
  };

  return (
    <div>
      {renderAdminInfo()}
      {renderPendingApprovalsTitle()}
      {pendingApprovals.length > 0 && renderPendingApprovalsTable()}
    </div>
  );
};
