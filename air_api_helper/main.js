
const EARTH_RADIUS = 6378137; // in meters (WGS84)
function offsetCoord(lat, lon, dx, dy) {
  const dLat = dy / EARTH_RADIUS;
  const dLon = dx / (EARTH_RADIUS * Math.cos((Math.PI / 180) * lat));

  const newLat = lat + (dLat * 180) / Math.PI;
  const newLon = lon + (dLon * 180) / Math.PI;

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
            console.log(data);

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

function get_map(coordx, coordy, precision) {
    coords1 = offsetCoord(coordx, coordy, -precision, -precision);
    coords2 = offsetCoord(coordx, coordy, precision, precision);

    const url = `https://api.openaq.org/v3/locations?parameters_id=2&bbox=${coords1.lat},${coords1.lon},${coords2.lat},${coords2.lon}&limit=${20}`;

    // const url = `https://api.openaq.org/v3/locations?parameters_id=2&bbox=${coordx},${coordy},${coords2.lat},${coords2.lon}&limit=${20}`;

      console.log(url);
    return fetchJson(url, {"X-API-Key": key}) 
    .then(data => {
        // console.log(url);
          // array = data.result;
          // let array2 = [];
          // array.forEach((element, index) => {
          //     console.log(`Index ${index}:`, element);
          // });
          // array2.sort((a, b) => {return a - b});
      })

      .catch(err => {
          // console.log(err);
          return {};
      });
}

get_map(-118.668153, 33.703935, 25000);

