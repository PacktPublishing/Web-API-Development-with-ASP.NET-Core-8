const gulp = require('gulp');  
const browserify = require('browserify');  
const source = require('vinyl-source-stream');  
const buffer = require('vinyl-buffer');  
const sourcemaps = require('gulp-sourcemaps');  
const tsify = require('tsify');  
  
// Bundle TypeScript with SignalR  
gulp.task('bundle', () => {  
  return browserify({  
    basedir: '.',  
    debug: true,  
    entries: ['src/app.ts'], // Replace with your TypeScript entry file  
    cache: {},  
    packageCache: {},  
  })  
    .plugin(tsify)  
    .bundle()  
    .pipe(source('bundle.js'))  
    .pipe(buffer())  
    .pipe(sourcemaps.init({ loadMaps: true }))  
    .pipe(sourcemaps.write('./'))  
    .pipe(gulp.dest('dist'));  
});  
  
// Copy HTML  
gulp.task('copy-html', () => {  
  return gulp.src('src/**/*.html')  
    .pipe(gulp.dest('dist'));  
});  
  
// Main build task  
gulp.task('default', gulp.series('bundle', 'copy-html'));  
