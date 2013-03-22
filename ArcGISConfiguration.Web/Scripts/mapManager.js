(function (mapManager) {
    "use strict";

    mapManager.map = null;
        
    mapManager.init = function (containerId, config) {

        require(["esri/map"], function () {

            if (config.proxyUrl)
                esri.config.defaults.io.proxyUrl = config.proxyUrl;

            mapManager.map = new esri.Map(containerId, config.options);
            if (config.extent)
                mapManager.map.setExtent(new esri.geometry.Extent(config.extent));
           
            var layers = config.layers.map(function (item)
            {
                return new esri.layers[item.type](item.url, item.options)
            });            
            mapManager.map.addLayers(layers);
        });
    }
})(window.mapManager = window.mapManager || {});

