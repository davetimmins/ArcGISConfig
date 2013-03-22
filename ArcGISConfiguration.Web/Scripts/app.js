require(["dojo/parser", "dojo/ready", "dojo/_base/xhr", "dijit/layout/BorderContainer", "dijit/layout/ContentPane"], function (parser, ready, xhr) {

    ready(function () {

        parser.parse();

        xhr.get({
            url: "/api/mapdata/" + role,
            handleAs: "json",
            headers: { "Content-Type": "application/json" },
            load: function (jsonData) {                                
                mapManager.init("map", jsonData);
            }
        });
    });
});