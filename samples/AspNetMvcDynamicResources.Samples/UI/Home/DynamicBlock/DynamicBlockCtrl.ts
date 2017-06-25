namespace aspNetMvcDynamicResources {

    namespace samples {

        class DynamicBlockCtrl {

            isUpdating: boolean;
            content: any;

            constructor(
                private mvcPrtialViewLoader: aspNetMvcDynamicResources.samples.MvcPrtialViewLoader,
                private $sce: angular.ISCEService) { }

            update() {
                this.isUpdating = true;
                this.mvcPrtialViewLoader.load("/DynamicBlock/Updated").then(html => {
                    this.content = this.$sce.trustAsHtml(html);
                }, () => {
                    alert("Ooops! It looks like something has gone terribly wrong");
                }).finally(() => {
                    this.isUpdating = false;
                });
            }
        }
        DynamicBlockCtrl.$inject = [aspNetMvcDynamicResources.samples.MvcPrtialViewLoader.ngName, "$sce"];

        app.controller("DynamicBlockCtrl", DynamicBlockCtrl);
    }
}