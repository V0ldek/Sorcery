// Trigger Asciinema players with the asciinema-player class.
document.addEventListener("DOMContentLoaded", (event) => {
    const players = document.getElementsByClassName("render-asciinema-player");
    for (const player of players) {
        const dimensions = {
            cols: player.dataset.cols,
            rows: player.dataset.rows,
        };
        AsciinemaPlayer.create(player.dataset.cast, player, dimensions);
    }
})