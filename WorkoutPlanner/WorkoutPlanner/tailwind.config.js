/**@type {import('tailwindcss').Config} */
module.exports = {
    content: [
      "./Pages/**/*.{razor,cshtml,html}",
      "./Shared/**/*.{razor,cshtml,html}",
      "./wwwroot/index.html",
      "./**/*.razor"
    ],
    theme: {
        extend: {
        },
    },
    plugins: [],
  }
  