# OpenLayers Map
OpenLayers is a free third party map location picker provider and can be found here: https://openlayers.org/
This map can be integrated to our website, zoomed in and out, and allows users to view existing sightings and drop a new sighting location on map by simply clicking on it. 

# Home Page
The map can be integrated on the homepage with marker points for whale sightings. The marker can also be a custom image such as a Whale icon.
When zoomed out: The sighting location markers can be clustered with a size propotional to the number of sightings.  
When zoomed in: Independent sighting location markers can be seen at a granular level using a distance parameter.

# Sightings Form Page
The user can leave a pin of their sighting's spot by simply clicking on the chosen point on the map. The location can then be retrieved in the form of latitude and longitude for the chosen point to be stored in the database.

# Useful Links
1. OpenLayers Tutorial: https://openlayers.org/doc/tutorials/concepts.html
2. Code used in the Map tutorial can be found here: https://www.npmjs.com/package/ol 
3. OpenLayers Library Documentation: https://openlayers.org/en/latest/apidoc/
4. Please refer to coordinates(for retrieving coordinates), point (for adding markers), and click event(to drop location).
5. Existing Map Prototype component has sample code for rendering the map and getting coordinated based on users click.  

# Commands to run
1. Run "npm install ol" to install OpenLayers Library