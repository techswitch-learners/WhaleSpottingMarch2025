import { useContext } from "react";
import { LoginContext } from "../../LoginManager/LoginManager";
import { useEffect, useState } from "react";
import {
  CreateReviewRequest,
  PendingApprovalModel,
  Species,
} from "../../../models/apiModels";
import "./Admin.scss";
import {
  getAllSpecies,
  getPendingApprovals,
  postReview,
} from "../../../utils/apiClient";
import { PendingApproval } from "../../pendingApproval/PendingApproval";

export const Admin = () => {
  const loginContext = useContext(LoginContext);
  const [pendingApprovals, setPendingApprovals] = useState<
    PendingApprovalModel[]
  >([]);
  const [speciesOptions, setSpeciesOptions] = useState<Species[]>([]);
  const [pendingApprovalsError, setPendingApprovalsError] = useState("");
  const [speciesLoadingError, setSpeciesLoadingError] = useState("");
  const [postReviewError, setPostReviewError] = useState("");
  const isLoggedInAdmin = loginContext.isLoggedIn && loginContext.isAdmin;

  useEffect(() => {
    async function fetchPendingApprovals() {
      try {
        const pendingApprovals = await getPendingApprovals();
        setPendingApprovals(pendingApprovals);
      } catch (error: unknown) {
        if (error instanceof Error) {
          setPendingApprovalsError(error.message);
        }
      }
    }

    if (isLoggedInAdmin) {
      fetchPendingApprovals();
    }
  }, [isLoggedInAdmin]);

  useEffect(() => {
    async function fetchSpecies() {
      try {
        const species = await getAllSpecies();
        setSpeciesOptions(species);
      } catch (error: unknown) {
        if (error instanceof Error) {
          setSpeciesLoadingError(error.message);
        }
      }
    }

    if (isLoggedInAdmin) {
      fetchSpecies();
    }
  }, [isLoggedInAdmin]);

  if (!isLoggedInAdmin) {
    return (
      <div>
        <h3> Only admin can view this page. Please log in as admin </h3>
        <h3>
          <a href="/LogIn"> Login </a>
        </h3>
      </div>
    );
  }

  const renderPendingApprovalsTitle = () => {
    const pendingApprovalsCount = pendingApprovals?.length;

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

  const handlePostReview = (request: CreateReviewRequest) => {
    postReview(request).then((status) => {
      if (status === 200) {
        const filtered = pendingApprovals.filter(
          (sighting) => sighting.id !== request.sightingId,
        );
        setPendingApprovals(filtered);
      } else {
        setPostReviewError("An error has occurred while posting reviews.");
      }
    });
  };

  const renderPendingApprovals = () => (
    <div>
      {pendingApprovals?.map((sighting) => (
        <PendingApproval
          key={`${sighting.id}-pending-approval`}
          sighting={sighting}
          speciesOptions={speciesOptions}
          onPostReview={handlePostReview}
        />
      ))}
    </div>
  );
  const renderError = () => (
    <>
      {!!pendingApprovalsError && (
        <div>
          <label className="admin__error">{pendingApprovalsError}</label>
        </div>
      )}
      {!!speciesLoadingError && (
        <div>
          <label className="admin__error">{speciesLoadingError}</label>
        </div>
      )}
      {!!postReviewError && (
        <div>
          <label className="admin__error">{postReviewError}</label>
        </div>
      )}
    </>
  );

  return (
    <div>
      {renderError()}
      {renderPendingApprovalsTitle()}
      {pendingApprovals?.length > 0 && renderPendingApprovals()}
    </div>
  );
};
