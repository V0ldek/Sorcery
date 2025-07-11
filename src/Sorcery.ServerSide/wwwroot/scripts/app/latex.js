// Trigger Katex rendering for all sections with the render-latex class.
document.addEventListener("DOMContentLoaded", (event) => {
    const configuration =
        {
            delimiters: [
                {
                    left: "$$",
                    right: "$$",
                    display: true
                },
                {
                    left: "$",
                    right: "$",
                    display: false
                }
            ],
            throwOnError: true
        };
    const containers = document.getElementsByClassName("render-latex");
    for (const container of containers) {
        renderMathInElement(container, configuration);
    }
});