document.addEventListener("DOMContentLoaded", (event) => {
    const sourceryTitleTag = document.getElementById('sourcery-title-tag');
    const actualTitleTag = document.getElementById('head-title-tag');
    actualTitleTag.innerHTML = sourceryTitleTag.dataset['title'];
});