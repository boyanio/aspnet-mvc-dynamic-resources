(() => {
    let showGreen = true;
    const toggleClass = () => {
        $("#dynamic-content").attr("class", showGreen ? "green" : "red");
        showGreen = !showGreen;
    };

    setInterval(toggleClass, 1500);
    toggleClass();
})();