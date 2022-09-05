const PROXY_CONFIG = [
  {
    context: [
      "/api/APIAccounts",
    ],
    target: "https://localhost:7073",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
