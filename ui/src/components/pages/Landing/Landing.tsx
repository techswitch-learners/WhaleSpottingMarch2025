import { useEffect, useState } from "react";

export const Landing = () => {
  // return <div>Landing page</div>;

  const [selectedFile, setSelectedFile] = useState<File | null>(null);
  const [preview, setPreview] = useState<string | null>(null);

  // create a preview as a side effect, whenever selected file is changed
  useEffect(() => {
    if (!selectedFile) {
      setPreview(null);
      return;
    }

    const objectUrl = URL.createObjectURL(selectedFile);
    setPreview(objectUrl);

    // free memory when ever this component is unmounted
    return () => URL.revokeObjectURL(objectUrl);
  }, [selectedFile]);

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event.target.files) {
      setSelectedFile(event.target.files[0]);
    }
  };

  const handleSubmit = async (
    event: React.FormEvent<HTMLFormElement>,
  ): Promise<void> => {
    event.preventDefault();

    const formData = new FormData();
    if (selectedFile) {
      console.log('selectedFile', selectedFile);
      
      formData.append("Description", "Details of Sighting");
      formData.append("image", selectedFile);
      formData.append("ImageSource", selectedFile.name);
      formData.append("species", "1");
      formData.append("sightingDate", "2025-03-01T13:21:33Z");
      formData.append("quantity", "1");
      formData.append("latitude", "42");
      formData.append("longitude", "-60");
      // const formData = new FormData();
      // const blob = new Blob([selectedFile], {
      //   type: "application/octet-stream",
      // });

      // const sightingRequest = {
      //   species: 1,
      //   description: "Details of Sighting",
      //   sightingDate: "2025-03-01T13:21:33Z",
      //   reportDate: "2025-03-02T13:21:33Z",
      //   quantity: 1,
      //   latitude: 41.9028,
      //   longitude: -60,
      // };

      // formData.append("sightingRequest", JSON.stringify(sightingRequest));
      // formData.append(
      //   "sightingRequest",
      //   new Blob([JSON.stringify(sightingRequest)], {
      //     type: "application/json",
      //   }),
      // );
      // formData.append("file", blob);
    }

    await fetch(
      import.meta.env.VITE_APP_API_HOST + "/Sighting/createSighting",
      {
        method: "POST",
        body: formData,
        // headers: new Headers({ "Content-Type": "multipart/form-data" }),
      },
    );
  };

  return (
    <form onSubmit={handleSubmit}>
      {selectedFile && preview && <img src={preview} />}
      <input type="file" onChange={handleFileChange} />
      <button type="submit">Submit</button>
    </form>
  );
};
