import { ChangeEvent, useState } from "react";
import {
  CreateReviewRequest,
  PendingApprovalModel,
  Species,
  UpdateSightingRequest,
} from "../../models/apiModels";
import "./PendingApproval.scss";

interface PendingApprovalProps {
  sighting: PendingApprovalModel;
  speciesOptions: Species[];
  onPostReview: (request: CreateReviewRequest) => void;
}

export const PendingApproval = (props: PendingApprovalProps) => {
  const { sighting, speciesOptions, onPostReview } = props;

  const [isEditable, setIsEditable] = useState<boolean>(false);
  const [description, setDescription] = useState<string>(sighting.description);
  const [selectedSpecies, setSelectedSpecies] = useState<Species>(
    // { id: sighting.speciesId, speciesName: sighting.speciesName }, // TODO Use this for sightingresponsmodel
    // after #16 is merged to main
    sighting.species,
  );
  const [comments, setComments] = useState<string>("");

  const handleApprove = (sightingId: number, approved: boolean) => {
    const request: CreateReviewRequest = {
      sightingId,
      approved,
      comments,
    };

    if (isEditable) {
      const updateSighting: UpdateSightingRequest = {
        species: selectedSpecies,
        description,
      };
      request.updatedSighting = updateSighting;
    }
    onPostReview(request);
  };

  const handleEdit = () => {
    if (isEditable) {
      setDescription(sighting.description);
      // const species: Species = { id: sighting.speciesId, speciesName: sighting.speciesName }
      // setSelectedSpecies(species); // TODO use this after #16 is merged to main
      setSelectedSpecies(sighting.species);
    }
    setIsEditable(!isEditable);
  };

  const handleDescriptionChange = (event: ChangeEvent<HTMLTextAreaElement>) => {
    setDescription(event.target.value);
  };

  const handleChangeSpecies = (event: ChangeEvent<HTMLSelectElement>) => {
    const species: Species = {
      id: Number(event.target.value),
      speciesName: event.target.options[event.target.selectedIndex].text,
    };
    setSelectedSpecies(species);
  };

  const handleCommentsChange = (event: ChangeEvent<HTMLTextAreaElement>) => {
    setComments(event.target.value);
  };

  const renderSpeciesOptions = () => (
    <select
      className="pending-approval__species"
      name="species"
      value={selectedSpecies.id}
      onChange={handleChangeSpecies}
      required
    >
      {speciesOptions.map(({ id, speciesName }) => (
        <option key={`admin-page-speciesName-${id}`} value={id}>
          {speciesName}
        </option>
      ))}
    </select>
  );

  return (
    <div className="pending-approval">
      <div className="pending-approval__item">{sighting.id}</div>
      <div className="pending-approval__item">
        {isEditable ? (
          <div>{renderSpeciesOptions()}</div>
        ) : (
          <div>{selectedSpecies.speciesName}</div>
        )}
      </div>
      <div className="pending-approval__item">
        {isEditable ? (
          <textarea
            id="description"
            onChange={handleDescriptionChange}
            value={description}
          ></textarea>
        ) : (
          <label>{description}</label>
        )}
      </div>
      <div className="pending-approval__item">
        <button onClick={handleEdit} className="edit-button">
          {isEditable ? "↩️" : "✏️"}
        </button>
      </div>
      <div className="pending-approval__item">
        <textarea
          id="comments"
          name="comments"
          value={comments}
          placeholder="Comments"
          onChange={handleCommentsChange}
        ></textarea>
      </div>
      <div className="pending-approval__item">
        <button
          onClick={() => handleApprove(sighting.id, true)}
          className="admin__approve"
        >
          Approve
        </button>
      </div>
      <div className="pending-approval__item">
        <button
          onClick={() => handleApprove(sighting.id, false)}
          className="admin__reject"
        >
          Deny
        </button>
      </div>
    </div>
  );
};
