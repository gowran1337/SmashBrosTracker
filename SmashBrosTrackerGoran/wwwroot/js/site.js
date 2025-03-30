
// This script is used to prevent the user from adding a player with a name that already exists in the list of players.
document.addEventListener("DOMContentLoaded", function () {
    const addPlayerForm = document.getElementById("add-player-form");
    const playerNameInput = document.getElementById("CreatePlayer");
    const nameError = document.getElementById("name-error");

    if (addPlayerForm) {
        addPlayerForm.addEventListener("submit", function (event) {
            event.preventDefault(); // Prevent default form submission

            const playerName = playerNameInput.value.trim();

            // Get the list of existing player names
            const existingPlayerNames = Array.from(document.querySelectorAll("#Player1NameInput option, #Player2NameInput option"))
                .map(option => option.textContent.trim());

            // Check if the player name already exists
            if (existingPlayerNames.includes(playerName)) {
                nameError.style.display = "block"; // Show the error message
            } else {
                nameError.style.display = "none"; // Hide the error message
                addPlayerForm.submit(); // Submit the form if the name is unique
            }
        });
    }
});


// This script is used to prevent the user from selecting the same player in both dropdowns.
document.addEventListener("DOMContentLoaded", function () {
    const player1Select = document.getElementById("Player1NameInput");
    const player2Select = document.getElementById("Player2NameInput");

    if (player1Select && player2Select) {
        // Function to disable the selected option in the other dropdown
        function updateDropdowns() {
            const player1SelectedId = player1Select.value;
            const player2SelectedId = player2Select.value;

            // Enable all options in both dropdowns first
            Array.from(player1Select.options).forEach(option => option.disabled = false);
            Array.from(player2Select.options).forEach(option => option.disabled = false);

            // Disable the selected option in the other dropdown
            if (player1SelectedId) {
                const player1Option = player2Select.querySelector(`option[value="${player1SelectedId}"]`);
                if (player1Option) player1Option.disabled = true;
            }

            if (player2SelectedId) {
                const player2Option = player1Select.querySelector(`option[value="${player2SelectedId}"]`);
                if (player2Option) player2Option.disabled = true;
            }
        }

        // Add event listeners to both dropdowns
        player1Select.addEventListener("change", updateDropdowns);
        player2Select.addEventListener("change", updateDropdowns);

        // Initialize the dropdowns on page load
        updateDropdowns();
    }
});


// This script is used to update the character images when the user selects a character for each player.
document.addEventListener("DOMContentLoaded", function () {
    const characterImages = {
        1: "/images/characters/fox.png",
        2: "/images/characters/falco.png",
        3: "/images/characters/marth.png",
        4: "/images/characters/sheik.png",
        5: "/images/characters/captainfalcon.png",
        6: "/images/characters/peach.png",
        7: "/images/characters/jigglypuff.png",
        8: "/images/characters/luigi.png"
    };

    function updateCharacterImages() {
        const p1Select = document.getElementById("P1Select");
        const p2Select = document.getElementById("P2Select");
        const p1Img = document.getElementById("P1CharacterImage");
        const p2Img = document.getElementById("P2CharacterImage");
        const vsImg = document.getElementById("VSImage");
        const chooseCharacterImage = document.getElementById("chooseCharacterImage");

        const p1CharacterId = p1Select.value;
        const p2CharacterId = p2Select.value;

        if (characterImages[p1CharacterId] && characterImages[p2CharacterId]) {
            // Hide the "Choose Your Character" image when both players have selected characters
            chooseCharacterImage.style.display = "none";

            // Show character images for both players
            p1Img.src = characterImages[p1CharacterId];
            p2Img.src = characterImages[p2CharacterId];

            p1Img.style.display = "block";
            p2Img.style.display = "block";

            // Show the "VS" image
            vsImg.style.display = "block";
        } else {
            // If either player has not selected a character, hide all images
            p1Img.style.display = "none";
            p2Img.style.display = "none";
            vsImg.style.display = "none";

            // Show the "Choose Your Character" image when either player hasn't selected
            chooseCharacterImage.style.display = "block";
        }
    }

    // Listen for changes in character selections
    document.getElementById("P1Select").addEventListener("change", updateCharacterImages);
    document.getElementById("P2Select").addEventListener("change", updateCharacterImages);
});


// This script is used to update the player names and character selections when the user clicks the "Start Session" button.
document.addEventListener("DOMContentLoaded", function () {
    const startSessionButton = document.querySelector(".start-session-button");
    startSessionButton.addEventListener("click", function () {
        document.getElementById("Player1Id").value = document.getElementById("Player1NameInput").value;
        document.getElementById("Player2Id").value = document.getElementById("Player2NameInput").value;
        document.getElementById("Player1CharacterId").value = document.getElementById("P1Select").value;
        document.getElementById("Player2CharacterId").value = document.getElementById("P2Select").value;
    });
});




