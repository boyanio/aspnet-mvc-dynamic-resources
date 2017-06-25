namespace aspNetMvcDynamicResources {

    export namespace samples {

        const registerStyleSheets = (styleSheets: string[]) => {
            const head = document.getElementsByTagName("head")[0];
            for (const styleSheet of styleSheets) {
                const link = document.createElement("link");
                link.rel = "stylesheet";
                link.type = "text/css";
                link.href = styleSheet;
                link.media = "all";
                head.appendChild(link);
            }
        };

        export class MvcPrtialViewLoader {

            static ngName = "mvcPrtialViewLoader";

            constructor(
                private $http: angular.IHttpService,
                private $q: angular.IQService) { }

            load(url: string) {
                const defer = this.$q.defer<string>();
                const requestConfig: angular.IRequestShortcutConfig = {
                    headers: {
                        "X-Requested-With": "XMLHttpRequest"
                    }
                };
                this.$http.get<string>(url, requestConfig).then(response => {
                    const contentType = response.headers("Content-Type");
                    if (contentType.indexOf("text/html") >= 0) {
                        const scripts = response.headers("X-Scripts");
                        if (scripts) {
                            this.$q.all(scripts.split(",").map(scriptUrl => this.$q.when($.getScript(scriptUrl))))
                                .then(() => {
                                    const styleSheets = response.headers("X-StyleSheets");
                                    if (styleSheets) {
                                        registerStyleSheets(styleSheets.split(","));
                                    }
                                    defer.resolve(response.data);
                                }, error => {
                                    defer.reject(error);
                                });
                        } else {
                            defer.resolve(response.data);
                        }
                    } else {
                        defer.reject("The response content type should be HTML");
                    }
                }, error => {
                    defer.reject(error);
                });
                return defer.promise;
            }
        }
        MvcPrtialViewLoader.$inject = ["$http", "$q"];

        app.service(MvcPrtialViewLoader.ngName, MvcPrtialViewLoader);
    }
}