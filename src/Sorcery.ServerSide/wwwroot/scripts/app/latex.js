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
    for (const container of document.getElementsByClassName("render-latex")) {
        renderMathInElement(container, configuration);
    }
});