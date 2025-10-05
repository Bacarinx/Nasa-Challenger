curl --request GET \
    --url "https://api.openaq.org/v3/locations?parameters_id=2&bbox=-118.668153,33.703935,-118.155358,34.337306&limit=1000"\
    --header "X-API-Key: 685a4eafe8ac47a89f1486b774c20dd33aa4acfe33659ee0b9978c92bf5396e0" > out.json && jq . out.json > outp.json


# ?bbox=5.488869,-0.396881,5.732144,-0.021973
# 
# https://api.openaq.org/v3/locations?bbox=-118.668153,33.703935,-118.155358,34.337306&limit=1000# https://api.openaq.org/v3/latest?bbox=-118.668153,33.703935,-118.155358,34.33730
#
# https://api.openaq.org/v3/parameters/2/latest?limit=1000
#
# https://api.openaq.org/v3/locations?parameters_id=2&coordinates=136.90610,35.14942&radius=12000&limit=5
#
