document.addEventListener("DOMContentLoaded", (event) => {
    const toTopButton = document.getElementsByClassName("mud-scroll-to-top")[0];
    const topOffset = 300;
    let visible = false;
    document.addEventListener("scroll", (event) => {
        if (!visible && document.documentElement.scrollTop >= topOffset) {
            visible = true;
            toTopButton.classList.add("visible");
            toTopButton.classList.remove("hidden");
        }
        else if (visible && document.documentElement.scrollTop === 0) {
            visible = false;
            toTopButton.classList.remove("visible");
            toTopButton.classList.add("hidden");
        }
    });
    toTopButton.addEventListener("click", (event) => {
        window.scrollTo({top: 0, behavior: 'smooth'});
    })
});
