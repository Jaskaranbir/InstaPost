// A simple utility to generate array for pagination
export default function (currentPage, width, totalPages) {
  currentPage = currentPage || 1
  if (!width) {
    throw new Error('Pagination "width" property undefined.')
  } else if (!totalPages) {
    throw new Error('Pagination "totalPages" property undefined.')
  }

  // Get the final page number (last page number)
  // We assume that current page number is the center.
  let finalPage = currentPage + Math.floor(width / 2)

  // If the total pages are less than pagination
  // count requested, prevent the negative numbers
  // by adding the factor to each pagination number.
  // ---------------------------------------------------
  // This factor should be the final factor.
  // It should be result of any preceeding or succeeding
  // factors already applied; to keep the final loop-code clean.
  let factor = (width > finalPage) ? (width - finalPage) : 0

  // If even pagination count is requested,
  // show more forward pages than backward pages
  const evenFix = (width % 2 === 0 ? 1 : 0)
  factor += evenFix

  // Limit results if end of total page count is reached
  // before pagination count.
  // Show more backward pages to keep the pagination count
  // consistent.
  if ((finalPage - factor) > totalPages) {
    finalPage = totalPages - evenFix
  }

  const pagination = []
  for (let i = 1; i <= width; i++) {
    const page = finalPage - (width - i) + factor

    if (page > 0 && page <= totalPages) {
      pagination.push(finalPage - (width - i) + factor)
    }
  }

  return pagination
}
