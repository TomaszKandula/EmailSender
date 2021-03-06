# [1.9.0](https://github.com/TomaszKandula/EmailSender/compare/v1.8.0...v1.9.0) (2022-02-08)


### Features

* upgrade libraries ([fdbecdc](https://github.com/TomaszKandula/EmailSender/commit/fdbecdce96e410b3cceeab262423ae28ab796f66))

# [1.8.0](https://github.com/TomaszKandula/EmailSender/compare/v1.7.0...v1.8.0) (2022-02-04)


### Features

* remove Sentry from project ([98d7236](https://github.com/TomaszKandula/EmailSender/commit/98d7236eb6c51bcc67e162aa07872c8b853ba16b))

# [1.7.0](https://github.com/TomaszKandula/EmailSender/compare/v1.6.0...v1.7.0) (2022-01-16)


### Features

* add cqrs for getting log files ([8202e84](https://github.com/TomaszKandula/EmailSender/commit/8202e84f51bac77a69b3c1eeefdf6e6bf58d49e1))
* add lgo information to handlers ([1ec9022](https://github.com/TomaszKandula/EmailSender/commit/1ec90227048c8b29bd55db124ff5b02b218bec18))
* add new endpoint for getting log files ([f8043c5](https://github.com/TomaszKandula/EmailSender/commit/f8043c58ea756de1b71da7f98e5d8634e046a9a6))
* add new error/validation codes ([5567115](https://github.com/TomaszKandula/EmailSender/commit/55671154b6382180839d6c02cfa9bf459937ade9))
* map root path ([a912f62](https://github.com/TomaszKandula/EmailSender/commit/a912f62bf28b12432c5f8bc65bcfdfb8b44033ae))

# [1.6.0](https://github.com/TomaszKandula/EmailSender/compare/v1.5.0...v1.6.0) (2022-01-15)


### Features

* add log information to handlers ([08ad0ad](https://github.com/TomaszKandula/EmailSender/commit/08ad0ad37b47f53b86c7d501e81efda5333a41b7))
* new logger configuration, add log to file ([e556917](https://github.com/TomaszKandula/EmailSender/commit/e556917ad4fabc2b95b683eb8329934030362639))

# [1.5.0](https://github.com/TomaszKandula/EmailSender/compare/v1.4.0...v1.5.0) (2022-01-02)


### Features

* add method to extract private key from the request header ([1829167](https://github.com/TomaszKandula/EmailSender/commit/18291674be51889b6fbb781693b96d4b3bf364dc))
* add new service for checking private key passed in the request header ([6dd69cc](https://github.com/TomaszKandula/EmailSender/commit/6dd69cc75f7f97e4f0fc642d45f88389b3ee850e))
* add private key in the request header ([e843cab](https://github.com/TomaszKandula/EmailSender/commit/e843cabcd31ab00192a85528af9501f2c20016d8))

# [1.4.0](https://github.com/TomaszKandula/EmailSender/compare/v1.3.0...v1.4.0) (2022-01-01)


### Bug Fixes

* correct path after folder restructure ([b7074ec](https://github.com/TomaszKandula/EmailSender/commit/b7074ecb0dcee4b1b7b08c74ff85a3dd9ee98266))


### Features

* add new behaviour service ([03dfff4](https://github.com/TomaszKandula/EmailSender/commit/03dfff4c4caad08d372cff0b2fbe44c53bccb504))

# [1.3.0](https://github.com/TomaszKandula/EmailSender/compare/v1.2.0...v1.3.0) (2021-12-27)


### Features

* add custom json converter for enums ([c698d95](https://github.com/TomaszKandula/EmailSender/commit/c698d95d42acafb69cfa90d8bab5317954bbd949))
* add endpoint for getting user emails ([5f96e24](https://github.com/TomaszKandula/EmailSender/commit/5f96e24743e7f23f7b22b08fcaf4a8c739f6338d))
* add new controllers and endpoints ([91ce81d](https://github.com/TomaszKandula/EmailSender/commit/91ce81dd98f59aaa3baacd9031df6b879848d758))
* use custom json converter and enum attribute for Unknown enum member ([c9b55e6](https://github.com/TomaszKandula/EmailSender/commit/c9b55e635d4a6e714a04b02fef7f904fd1ac78c4))

# [1.2.0](https://github.com/TomaszKandula/EmailSender/compare/v1.1.0...v1.2.0) (2021-12-21)


### Features

* downgrade SQLite and SQL to 5.0.13 until the issue with 6.0.1 is resolved ([4bc40a3](https://github.com/TomaszKandula/EmailSender/commit/4bc40a31dc4734effa35843b08e1698348581edc))

# [1.1.0](https://github.com/TomaszKandula/EmailSender/compare/v1.0.0...v1.1.0) (2021-12-19)


### Bug Fixes

* change to NET6 ([2dcddad](https://github.com/TomaszKandula/EmailSender/commit/2dcddadfdb91ea4a4115c8c991b4c844107aad70))


### Features

* upgrade to NET6, upgrade libraries ([3303f01](https://github.com/TomaszKandula/EmailSender/commit/3303f01af66c0eca53d05f6f9b0b564938bd13a2))

# 1.0.0 (2021-12-11)


### Bug Fixes

* ad missing property rule ([6e329f8](https://github.com/TomaszKandula/EmailSender/commit/6e329f81d5414cc8b59f2c9f984f6e87791901f9))
* add missing 'needs: versioning' ([44b14e7](https://github.com/TomaszKandula/EmailSender/commit/44b14e78a9e112b188f13eff48e493f1f47184e3))
* add missing entities ([0888a05](https://github.com/TomaszKandula/EmailSender/commit/0888a0592072bdd0bbadb8b72a4413e49f40e17d))
* add version number, use string interpolation ([4098398](https://github.com/TomaszKandula/EmailSender/commit/4098398d1d1701fc54b799c194b2acd77c798394))
* correct argument name to match implementation ([6fc0215](https://github.com/TomaszKandula/EmailSender/commit/6fc0215f78331413d6618f2f7f5ae55e6fd857ba))
* correct controller class name ([d60ecb8](https://github.com/TomaszKandula/EmailSender/commit/d60ecb8d30d54393ef1ebf0e0fdba5055cc11bc6))
* correct workflow script ([7177613](https://github.com/TomaszKandula/EmailSender/commit/71776139a3e56bf523088e74cf512266c638332e))
* move project one folder up ([9bbb27e](https://github.com/TomaszKandula/EmailSender/commit/9bbb27eb9f5bca97a5b88e068ab675981cdf75f4))
* remove unused instance, remove unnecessary boolean ([d603e72](https://github.com/TomaszKandula/EmailSender/commit/d603e723cf0b823376ef823f3806bb5991fb151d))
* use random generator which are recommended by OWASP ([cad2a7d](https://github.com/TomaszKandula/EmailSender/commit/cad2a7d2fc841d70c3bff1517d4d6d82a2c2ae12))


### Features

* ad models for new endpoint and handler ([a778855](https://github.com/TomaszKandula/EmailSender/commit/a7788553598378ae9b9cc0e7a72ad7fdecddb0d2))
* add api version to controllers ([1a2e5e0](https://github.com/TomaszKandula/EmailSender/commit/1a2e5e0c32c0bdde6060a166c8947d34f1f92ece))
* add assembly reference ([1264ce6](https://github.com/TomaszKandula/EmailSender/commit/1264ce672d65ccfc0f99d32834894c27bb6d904c))
* add base controller for all API controllers ([db275fd](https://github.com/TomaszKandula/EmailSender/commit/db275fd1965a742a86d54ae7446a3d265fbb4dc1))
* add behaviours ([d5545a6](https://github.com/TomaszKandula/EmailSender/commit/d5545a694f855f4eb04de1205b9feb536d60c0f2))
* add billing controller ([0332600](https://github.com/TomaszKandula/EmailSender/commit/0332600fdde89235b172805b2c078d1ee627075d))
* add billing implementation ([8023567](https://github.com/TomaszKandula/EmailSender/commit/80235670be47c1ce9e8250d7e8e471071de691c8))
* add billing table ([df7da87](https://github.com/TomaszKandula/EmailSender/commit/df7da876a9fc84cb453de784d93b9cd6a57f815c))
* add common models in shared service ([2a74a25](https://github.com/TomaszKandula/EmailSender/commit/2a74a25b4e531d2f263cce9d1d92a6d31d54b120))
* add controller with initial endpoint for sending email ([e4588fb](https://github.com/TomaszKandula/EmailSender/commit/e4588fb6886634811b8521dda88575334d0bf0d4))
* add CORS headers to configuration ([cb0dfbd](https://github.com/TomaszKandula/EmailSender/commit/cb0dfbdc4b5cf611cb854ef1083ec0758e14792e))
* add custom middleware ([d13170a](https://github.com/TomaszKandula/EmailSender/commit/d13170ad35ba08351a6cc938c46741e38ae34074))
* add database context ([aa131f5](https://github.com/TomaszKandula/EmailSender/commit/aa131f599ff2690adb40a8f3a78dedf5bf1ea6bf))
* add database initializer ([86c603d](https://github.com/TomaszKandula/EmailSender/commit/86c603daa07405f83078a87873b421222cf92a53))
* add database mappings ([60c29a6](https://github.com/TomaszKandula/EmailSender/commit/60c29a67a8ce172a3aa96c4872b6cf7ea8a2ffe8))
* add dependencies to configuration ([55d8054](https://github.com/TomaszKandula/EmailSender/commit/55d80548a095388ba4082b02c0d640cb0811f102))
* add dockerfile, dockerignore and test-run script ([0138da0](https://github.com/TomaszKandula/EmailSender/commit/0138da0e56451271857bcfcaf8d55af3d514beae))
* add domain entities ([3d452be](https://github.com/TomaszKandula/EmailSender/commit/3d452beee469ead12c8bce89377839afa4deddef))
* add domain enums ([1999cc8](https://github.com/TomaszKandula/EmailSender/commit/1999cc84134830ef36700a3e67846bc656941be2))
* add DTO and Validator ([ec468dd](https://github.com/TomaszKandula/EmailSender/commit/ec468dd98e6bb88e18577f80d5848b7da0064b83))
* add DTO model for new POST endpoint ([3c64c6d](https://github.com/TomaszKandula/EmailSender/commit/3c64c6d7ced96d896053a0f59e284263f97f9948))
* add email controller mapper ([422697e](https://github.com/TomaszKandula/EmailSender/commit/422697ed6454485fa5e4a688fddfc21ab5ea0082))
* add email verification that checks format and domain ([f9356c0](https://github.com/TomaszKandula/EmailSender/commit/f9356c0f3d22e9358311a236742cee1cededa30a))
* add endpoint for getting allow domains ([6ffc667](https://github.com/TomaszKandula/EmailSender/commit/6ffc6679859002f4d3bab825a4e0995e7277d795))
* add endpoint for GetUserDetails ([af9e063](https://github.com/TomaszKandula/EmailSender/commit/af9e0639f64d31e2ea1ccafecf30d5af4523041b))
* add endpoint to get allow emails ([efb26c7](https://github.com/TomaszKandula/EmailSender/commit/efb26c7e7a7a406b955abafdd2201afc7fdca72e))
* add endpoint VerifyEmail ([d500a92](https://github.com/TomaszKandula/EmailSender/commit/d500a9251d41a1c74fb69d729a600f38f1a3e146))
* add ENV information ([8fa9e8f](https://github.com/TomaszKandula/EmailSender/commit/8fa9e8f518275c8daff09a3a24148b7c77a18ff7))
* add error codes ([31b7e80](https://github.com/TomaszKandula/EmailSender/commit/31b7e8004ac8055fd4f6566abc927e55cb4c9234))
* add error result model ([b8efce5](https://github.com/TomaszKandula/EmailSender/commit/b8efce5030162cc8c9e7cb6777ad10aef475b9c4))
* add ETag attribute filter ([a739ec5](https://github.com/TomaszKandula/EmailSender/commit/a739ec5d0c47dbe6b46806ff3a0dedc50fd5312f))
* add exceptions in shared service ([0b5a413](https://github.com/TomaszKandula/EmailSender/commit/0b5a4135a81a332431cccb2d311925984458979b))
* add extension methods ([6da98c8](https://github.com/TomaszKandula/EmailSender/commit/6da98c8331ff63941b74d5674bbecc9b8cfefefe))
* add first unit tests ([ddf600b](https://github.com/TomaszKandula/EmailSender/commit/ddf600be636084aef2e57df3f843e4f775b81654))
* add GetSentHistory endpoint ([823d282](https://github.com/TomaszKandula/EmailSender/commit/823d282104e39e6b9ecc4b91c98f1910784e9d05))
* add handler and validator ([a7df257](https://github.com/TomaszKandula/EmailSender/commit/a7df2575579e56583e3e5d492e16bcaa8558c2f6))
* add handler and validator for GetUserDetails ([ada0c88](https://github.com/TomaszKandula/EmailSender/commit/ada0c88d9f370db33fdb75029181fdf2304e8697))
* add handler and validator for VerifyEmail action ([db61358](https://github.com/TomaszKandula/EmailSender/commit/db613588668b46805e552ec6d823ba77ac9e7f3b))
* add ILogger to abstract away logger of choice ([9bda490](https://github.com/TomaszKandula/EmailSender/commit/9bda4904c840615338d07d4e73c19fafc13460dc))
* add initial database migration ([b2601c7](https://github.com/TomaszKandula/EmailSender/commit/b2601c7d0c485e5d31a62c4d86e2037a7ec09a3e))
* add logging behaviour ([041042c](https://github.com/TomaszKandula/EmailSender/commit/041042cca43077678710e0eccf587c202c2dce76))
* add middleware ([ec2c794](https://github.com/TomaszKandula/EmailSender/commit/ec2c79483a3af060866c3fefa7c5a6da7c2d285f))
* add middleware implementations ([2b3fa89](https://github.com/TomaszKandula/EmailSender/commit/2b3fa89dcbc2c04a65ace58b7e96edad442d8cb2))
* add model for email verification result ([688cbe6](https://github.com/TomaszKandula/EmailSender/commit/688cbe69eb3f304a4d492993e3ca640264c9640d))
* add model, handler and validator for GetServerStatus ([b996670](https://github.com/TomaszKandula/EmailSender/commit/b9966705c4066e519f4d22a49f7165a7c0c76ab0))
* add models for GetUserDetails handler ([42f338e](https://github.com/TomaszKandula/EmailSender/commit/42f338e2b2f119d213eadc1e809ac16de961538f))
* add models for new endpoint ([02db023](https://github.com/TomaszKandula/EmailSender/commit/02db023f97d4a4ecb5779cf90591b0522dd6f14e))
* add models for response and request ([6d5d986](https://github.com/TomaszKandula/EmailSender/commit/6d5d986d8eaf000c28512abecd2776719c03539f))
* add new column to database, add migration ([11f7c73](https://github.com/TomaszKandula/EmailSender/commit/11f7c7332254d5ef1f8b3360f7b2060bbb5b5f8f))
* add new columns to Email database table ([fd22fa1](https://github.com/TomaszKandula/EmailSender/commit/fd22fa14dcda7b08e08e710c2ea5bcef5d2255e2))
* add new database entity ([ce4263a](https://github.com/TomaszKandula/EmailSender/commit/ce4263ab91d55f00b7b4e24bea89843da23a8d39))
* add new database table for regex patterns ([eac4a8f](https://github.com/TomaszKandula/EmailSender/commit/eac4a8fc1d83d16aaed2233688e2c0d8dbaf83c8))
* add new database table, ad migration ([a7922fe](https://github.com/TomaszKandula/EmailSender/commit/a7922fe8a437de7872a381202f909336eed4dbc9))
* add new DI service ([ba6da99](https://github.com/TomaszKandula/EmailSender/commit/ba6da991ef5d4aecc7c3144de2a2b4c68799cb09))
* add new email service ([cec3026](https://github.com/TomaszKandula/EmailSender/commit/cec30260b2d7653b3f3ee3286bdd5677fed51624))
* add new endpoint ([63c892e](https://github.com/TomaszKandula/EmailSender/commit/63c892eb522d0fe5671122dbf2cc50909ca76eb9))
* add new endpoint GetServerStatus ([c639023](https://github.com/TomaszKandula/EmailSender/commit/c63902367c93d505ea9d2a481b67e225e730ac36))
* add new error code ([7914a14](https://github.com/TomaszKandula/EmailSender/commit/7914a146169fc121947695f072b889aa34aea8b3))
* add new error code ([e76c166](https://github.com/TomaszKandula/EmailSender/commit/e76c1667438997b750a74dc95ea45bb01fdcdc4d))
* add new error code ([d51d723](https://github.com/TomaszKandula/EmailSender/commit/d51d723f56c314700e51ca72d76c382a47b9966c))
* add new error code for api versioning, remove unused error codes ([8820f0e](https://github.com/TomaszKandula/EmailSender/commit/8820f0eee7cb1128e07b0c02e84850bdcaae701c))
* add new error code for missing pricing ([f03e56d](https://github.com/TomaszKandula/EmailSender/commit/f03e56d56648d4eab4b2172da82ee95f1d1eea59))
* add new error codes ([0722bd5](https://github.com/TomaszKandula/EmailSender/commit/0722bd55d6c5eaf8592367bfeceedc4963c226ba))
* add new error codes ([bb36500](https://github.com/TomaszKandula/EmailSender/commit/bb36500f036deb05c42ea2c93375b7b472d20bae))
* add new errors to resources ([45ba7f9](https://github.com/TomaszKandula/EmailSender/commit/45ba7f90e8652f0bbbca200d172f5f56fded672f))
* add new exception ([7a8ce82](https://github.com/TomaszKandula/EmailSender/commit/7a8ce82eec917f9ca66993466e3a8574c2292d92))
* add new exceptions ([9116e37](https://github.com/TomaszKandula/EmailSender/commit/9116e3794e7c69eaf8730cfba9bc1a2173407b9a))
* add new handler and validator for getting history of actions ([67820ff](https://github.com/TomaszKandula/EmailSender/commit/67820ff5b167e4bfe71c0ad95f167e92efb65982))
* add new handler for returning registered domains ([9e8cec9](https://github.com/TomaszKandula/EmailSender/commit/9e8cec9f7fd9494676865661705303d4a227e01b))
* add new handler, models and validator ([6112502](https://github.com/TomaszKandula/EmailSender/commit/611250268f5c00f3a8266e29e4780c1fda9c6cd7))
* add new handler, models and validator for user billing ([1a122b4](https://github.com/TomaszKandula/EmailSender/commit/1a122b42270862a8fda9c74cf8f54ac812dfc5c7))
* add new helper service ([3ac723b](https://github.com/TomaszKandula/EmailSender/commit/3ac723bffcb3256f5dc47c8eb410c9549d66731f))
* add new helper service for handling date and time ([f427c4b](https://github.com/TomaszKandula/EmailSender/commit/f427c4bb6a6a6708882de63127097870a99c8b42))
* add new method to retrieve all user billings ([bb5aa99](https://github.com/TomaszKandula/EmailSender/commit/bb5aa99652e1000ef0c0c349ac1e5d56ac742a44))
* add new methods ([9925c13](https://github.com/TomaszKandula/EmailSender/commit/9925c1387669567c17b80ed2aefb006b7e3fc926))
* add new model to hold Polish VAT options ([0533e15](https://github.com/TomaszKandula/EmailSender/commit/0533e151e79cf737f9f03a42a09ce8c2e6d6cc66))
* add new models ([d60cc28](https://github.com/TomaszKandula/EmailSender/commit/d60cc288d10c437a3b909af70bca505041470318))
* add new models ([1c0492a](https://github.com/TomaszKandula/EmailSender/commit/1c0492acd4d9f36bfb2495922c7db1cbbf9edbb4))
* add new models for request and response ([829ebb4](https://github.com/TomaszKandula/EmailSender/commit/829ebb43991089e6303ada7517bcb50aa5a7d11a))
* add new packages and services ([ce39857](https://github.com/TomaszKandula/EmailSender/commit/ce39857056897e51bf2538db0b8204fac54823a4))
* add new project to a solution ([c433572](https://github.com/TomaszKandula/EmailSender/commit/c4335728a0a026fdece360c5aa2297b2a9d5c1f4))
* add new service for sending emails ([54365ae](https://github.com/TomaszKandula/EmailSender/commit/54365ae1c9085875c51b2b289b6ccbf592d85c1e))
* add new services without implementation ([d57ddf5](https://github.com/TomaszKandula/EmailSender/commit/d57ddf5fd2b76dece88d3c81dcbceb591652c2fe))
* add new tests for validators ([5940fa1](https://github.com/TomaszKandula/EmailSender/commit/5940fa1ad3b52e5e6de99abfe03df7a6b90de52b))
* add new validation rules ([ed8c7fc](https://github.com/TomaszKandula/EmailSender/commit/ed8c7fc3e19869aff40b98e382e3d252c95a3905))
* add new validator for date time range ([3de786f](https://github.com/TomaszKandula/EmailSender/commit/3de786f8d2bc7f77975052c96063f2c5e672d456))
* add price table ([e8d5d43](https://github.com/TomaszKandula/EmailSender/commit/e8d5d43e81f28f941c31f90fe0a5a90242f9b8a1))
* add project for backend unit tests ([a69f618](https://github.com/TomaszKandula/EmailSender/commit/a69f618c1c9b3ce0ed4b6d1c6361981d3e4312b4))
* add project reference ([b923cea](https://github.com/TomaszKandula/EmailSender/commit/b923cea112e06c62a4f06ac473c17c82342dca0a))
* add references ([414cf9c](https://github.com/TomaszKandula/EmailSender/commit/414cf9cf798daa601236736dc1a6512fda8b4a06))
* add request, handler and validator for action ([12aec04](https://github.com/TomaszKandula/EmailSender/commit/12aec04271534787df8f37a35d01fe98af8042a2))
* add resources for validation errors and other errors ([d3ffcc4](https://github.com/TomaszKandula/EmailSender/commit/d3ffcc4353671bcba2db5bbf6277289155e3928a))
* add semantic release ([26c6e21](https://github.com/TomaszKandula/EmailSender/commit/26c6e214e8612d5a765a13dbc962a0767ff9ee33))
* add sender service tests ([0eab147](https://github.com/TomaszKandula/EmailSender/commit/0eab14790f621cbc5911803208249e4995abb642))
* add server exception with 500 status code ([537618e](https://github.com/TomaszKandula/EmailSender/commit/537618e589202256e4984da43bb68a9dd00d756f))
* add setting model ([8b7d4d4](https://github.com/TomaszKandula/EmailSender/commit/8b7d4d4356fa42bd1adf065d1d969ca20c78fbae))
* add Shared backend service ([c0662e4](https://github.com/TomaszKandula/EmailSender/commit/c0662e46e131a611e93d585b1a4b4e9a0278344d))
* add shared service ([0a8e494](https://github.com/TomaszKandula/EmailSender/commit/0a8e494495cda9e74803859387d7989ba393ec92))
* add standard pricing database table, add migration ([d684d19](https://github.com/TomaszKandula/EmailSender/commit/d684d198e75f77d27bdbeecebf267a0607ea57a1))
* add string extensions ([8f412a7](https://github.com/TomaszKandula/EmailSender/commit/8f412a765448dd0731d12f7b71f913a4b888e8df))
* add template handler for any MediatR handler ([c9d7c42](https://github.com/TomaszKandula/EmailSender/commit/c9d7c42ff6aae52ac63c8ef3203fe76eb0eeeada))
* add test base and database factory for unit tests ([fecaac4](https://github.com/TomaszKandula/EmailSender/commit/fecaac45229041e4a5e28fa058011de2e862506a))
* add to configuration both handlers and swagger ([a52d0d0](https://github.com/TomaszKandula/EmailSender/commit/a52d0d0597f67d6712d985487d28d9c7204f5e14))
* add user details table, add migration ([8118937](https://github.com/TomaszKandula/EmailSender/commit/8118937abc9bf5ec262eb0a962d206dc709407ad))
* add validation rule for property ([d4f3782](https://github.com/TomaszKandula/EmailSender/commit/d4f3782e3128157d8dc5459ba24e7101ac0aaadc))
* add VAT validation service ([6c9f1f1](https://github.com/TomaszKandula/EmailSender/commit/6c9f1f156fc1df57864a93c16eed4baca486820b))
* add verification functionality ([698e5e7](https://github.com/TomaszKandula/EmailSender/commit/698e5e76c6e8d55b702eade194a09bebd43ad6ce))
* change table name ([9252230](https://github.com/TomaszKandula/EmailSender/commit/9252230ab1c2a75f2dc2c2f36e857df8b0d655d9))
* initial commit, add projects to the solution folders ([da7caa9](https://github.com/TomaszKandula/EmailSender/commit/da7caa9aba1a23cc37ec63fa3279df092f85adbb))
* register requests in history table ([48093b1](https://github.com/TomaszKandula/EmailSender/commit/48093b1d7b63e5fe91b006197bc29907414f1d3c))
* rename table and add columns, add migration ([41c2c95](https://github.com/TomaszKandula/EmailSender/commit/41c2c95fa258cc2f2d2b87c676d48e7bcc8f3cd2))
* replace forcing headers by framework cors configuration extension ([b24401a](https://github.com/TomaszKandula/EmailSender/commit/b24401ab5b4ae81221ff58e1060321aae9bb019f))
* setup api versioning configuration with custom error response ([2dadc66](https://github.com/TomaszKandula/EmailSender/commit/2dadc66addfa382ff8d30b4b64b425959530a939))
