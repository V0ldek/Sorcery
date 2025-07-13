document.addEventListener("DOMContentLoaded", (event) => {
    const layout = document.getElementById("mud-layout");
    const drawer = document.getElementById("nav-drawer");
    const toggleButton = document.getElementById("drawer-toggle-button");
    let initial = true;
    
    function setDrawerState(open) {
        initial = false;
        drawer.classList.remove('mud-drawer--initial');
        if (open) {
            layout.classList.add('mud-drawer-open-responsive-md-left');
            layout.classList.remove('mud-drawer-closed-responsive-md-left')
            drawer.classList.add('mud-drawer--open');
            drawer.classList.remove('mud-drawer--closed');
        } else {
            layout.classList.remove('mud-drawer-open-responsive-md-left')
            layout.classList.add('mud-drawer-closed-responsive-md-left');
            drawer.classList.remove('mud-drawer--open');
            drawer.classList.add('mud-drawer--closed');
        }
    }
    
    toggleButton.addEventListener('click', () => {
        const isOpen = drawer.classList.contains('mud-drawer--open') && 
            (!initial || window.getComputedStyle(drawer).display !== 'none');
        setDrawerState(!isOpen);
    });
});