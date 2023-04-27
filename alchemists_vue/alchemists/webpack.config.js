const webpack = require('webpack');

module.exports = {
  mode: 'development',
  entry: {
    app: './src/App.vue',
  },
  output: {
    path: './dist',
    filename: 'alchemist_main.js',
    publicPath: './',
  },
  module: {

  },
  plugins: [],
  optimization: {},
  resolve: {
    fallback: {
        fs: false,
        net: false,
        stream: require.resolve('stream-browserify'),
        crypto: require.resolve('crypto-browserify'),
        http: require.resolve('stream-http'),
        https: require.resolve('https-browserify'),
    }
  },
};