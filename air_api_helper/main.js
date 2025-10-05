
const EARTH_RADIUS = 6378137; // in meters (WGS84)
function offsetCoord(lat, lon, dx, dy) {
  const dLat = dy / 111320;
  const dLon = dx / (111320 * Math.cos((Math.PI / 180) * lat));

  const newLat = lat + dLat;
  const newLon = lon + dLat;

  return { lat: newLat, lon: newLon };
}

function distanceXY(lat1, lon1, lat2, lon2) {
  const toRad = deg => deg * Math.PI / 180;

  const dLat = lat2 - lat1;
  const dLon = lon2 - lon1;

  const dy = dLat * (Math.PI / 180) * EARTH_RADIUS;
  const dx = dLon * (Math.PI / 180) * EARTH_RADIUS * Math.cos(toRad((lat1 + lat2) / 2));

  return { dx, dy };
}

async function fetchJson(url, headers = {}) {
  if (typeof window === 'undefined') {
    // Node.js
    const https = require('https');
    const { URL } = require('url');

    return new Promise((resolve, reject) => {
      const urlObj = new URL(url);

      const options = {
        hostname: urlObj.hostname,
        path: urlObj.pathname + urlObj.search,
        method: 'GET',
        headers: headers,
      };

      const req = https.request(options, (res) => {
        let data = '';

        res.on('data', (chunk) => {
          data += chunk;
        });

        res.on('end', () => {
          try {
            // console.log(data);

            const json = JSON.parse(data);
            resolve(json);
          } catch (err) {
            reject(new Error('Invalid JSON: ' + err.message));
          }
        });
      });

      req.on('error', (err) => {
        reject(err);
      });

      req.end();
    });
  } else {
    // Browser
    const response = await fetch(url, {
      method: 'GET',
      headers: headers
    });

    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }

    return await response.json();
  }
}
const key = "685a4eafe8ac47a89f1486b774c20dd33aa4acfe33659ee0b9978c92bf5396e0";

async function get_ids(coordx, coordy, precision) {
    let coords1 = offsetCoord(coordx, coordy, -precision, -precision);
    let coords2 = offsetCoord(coordx, coordy, precision, precision);

    const url = `https://api.openaq.org/v3/locations?parameters_id=2&bbox=${coords1.lat},${coords1.lon},${coords2.lat},${coords2.lon}&limit=${20}`;

      console.log(url);
      let ids = [];
      await fetchJson(url, {"X-API-Key": key}) 
    .then(data => {
          data.results.forEach((element, index) => {
              console.log(`Index ${index}:`, element.id);
              ids.push(id);
          });
          // return ids;
      })

      .catch(err => {
          // console.log(err);
          return [];
      });
      return ids;

}

async function get_map(coordx, coordy, precision) {
    const ids = await get_ids(coordx, coordy, precision);
    let array = [];

        console.log(ids);
    ids.forEach((id, index) => {
        // console.log(ids);
        const url = `https://api.openaq.org/v3/locations/${id}/latest?limit=${1}`;
        array.push(fetchJson(url, {"X-API-Key": key}) 
        .then(data => {
              // console.log(err);
              console.log(data);
              console.log(data, "  aquiiiii");
          })

          .catch(err => {
              console.log(err,  "  aquiiiii");
              return [];
          }))
    });

    await Promise.all(array);

}

get_map(-118.6681, 33.7039, 50000);

