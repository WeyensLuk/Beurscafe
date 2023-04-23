const PROXY_CONFIG = [
  {
    context: [
      "/drinks",
    ],
    target: "https://localhost:7111",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
